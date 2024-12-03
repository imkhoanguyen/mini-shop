import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { DropdownModule } from 'primeng/dropdown';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { OrderService } from '../../../_services/order.service';
import { ToastrService } from '../../../_services/toastr.service';
import { Router } from '@angular/router';
import { UtilityService } from '../../../_services/utility.service';
import { Order, OrderParams } from '../../../_models/order';
import { Pagination } from '../../../_models/pagination';
import { AccountService } from '../../../_services/account.service';

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
    CalendarModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
    ButtonModule,
  ],
  templateUrl: './order-client.component.html',
  styleUrl: './order-client.component.css',
  providers: [ConfirmationService],
})
export class OrderClientComponent {
  private orderService = inject(OrderService);
  private toastrService = inject(ToastrService);
  private router = inject(Router);
  private accountService = inject(AccountService);
  utilService = inject(UtilityService);
  constructor(private confirmationService: ConfirmationService) {}
  orders: Order[] = [];
  prm = new OrderParams();
  pagination: Pagination | undefined;
  selectedPaymentStatus = '';
  selectedStatus = '';
  rangeDates: Date[] | undefined;

  ngOnInit(): void {
    this.loadOrders();
  }
  loadOrders() {
    this.prm.userId = this.accountService.getCurrentUser()?.id || '';

    if (this.prm.userId) {
      this.orderService.GetAllWithLimit(this.prm).subscribe({
        next: (res) => {
          if (res.items && res.pagination) {
            this.orders = res.items;
            this.pagination = res.pagination;
          }
          console.log(res);
        },
      });
    } else {
      this.toastrService.error('vui lòng đăng nhập lại');
    }
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
          this.toastrService.error('Không tìm thấy orderId');
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

  onReset() {
    this.prm.selectedStatus = '';
    this.prm.selectedPaymentStatus = '';
    this.prm.endDate = '';
    this.prm.startDate = '';
    this.rangeDates = undefined;
    this.prm.pageNumber = 1;
    this.loadOrders();
  }

  onApply() {
    if (this.selectedPaymentStatus) {
      this.prm.selectedPaymentStatus = this.selectedPaymentStatus;
    }

    if (this.selectedStatus) {
      this.prm.selectedStatus = this.selectedStatus;
    }

    // Xử lý rangeDates
    if (this.rangeDates && this.rangeDates.length === 2) {
      const [start, end] = this.rangeDates;

      // Format ngày thành chuỗi dạng yyyy-MM-dd HH:mm:ss
      this.prm.startDate = start ? this.formatDateToStringWithTime(start) : '';
      this.prm.endDate = end ? this.formatDateToStringWithTime(end) : '';
    } else {
      this.prm.startDate = '';
      this.prm.endDate = '';
    }

    this.loadOrders();
  }

  private formatDateToStringWithTime(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Tháng bắt đầu từ 0
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
  }

  openOrderDetail(orderId: number) {
    console.log(orderId);
    this.router.navigate(['/order', orderId]);
  }
}
