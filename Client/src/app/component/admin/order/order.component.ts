import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../../_services/order.service';
import { FormsModule } from '@angular/forms';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { Order, OrderParams } from '../../../_models/order';
import { Pagination } from '../../../_models/pagination';
import { ToastrService } from '../../../_services/toastr.service';
import { ConfirmationService } from 'primeng/api';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    FormsModule,
    ConfirmPopupModule,
    PaginatorModule,
    TableModule,
    CommonModule,
    DropdownModule,
  ],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css',
  providers: [ConfirmationService],
})
export class OrderComponent implements OnInit {
  private orderService = inject(OrderService);
  private toastrService = inject(ToastrService);
  constructor(private confirmationService: ConfirmationService) {}
  orders: Order[] = [];
  prm = new OrderParams();
  pagination: Pagination | undefined;
  ngOnInit(): void {
    this.loadOrders();
  }
  loadOrders() {
    this.orderService.GetAllWithLimit(this.prm).subscribe({
      next: (res) => {
        if (res.items && res.pagination) {
          this.orders = res.items;
          this.pagination = res.pagination;
        }
        console.log(res);
      },
    });
  }
  calTotal(order: Order) {
    const discountPrice = Number(order.discountPrice) || 0;
    return Number(order.subTotal) + Number(order.shippingFee) - discountPrice;
  }
  onPageChange(event: any) {
    this.prm.pageNumber = event.page + 1;
    this.prm.pageSize = event.rows;
    this.loadOrders();
  }
  onSearch() {
    // reset pageNumber
    this.prm.pageNumber = 1;
    this.loadOrders();
  }
  deleteConfirmPopup(event: Event, orderId: number) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bạn muốn xóa dòng này?',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-sm',
      accept: () => {
        if (orderId < 1) {
          this.toastrService.error('Không tìm thấy roleId');
          return;
        }
        this.orderService.deleteOrder(orderId).subscribe({
          next: (_) => {
            const index: number = this.orders.findIndex(
              (o) => o.id === orderId
            );
            this.orders.splice(index, 1);
            this.toastrService.success('Xóa thành công');
          },
          error: (error) => console.log(error),
        });
      },
      reject: () => {
        this.toastrService.info('Bạn đã hủy xóa');
      },
    });
  }
  openOrderDetail(orderId: number) {}
}
