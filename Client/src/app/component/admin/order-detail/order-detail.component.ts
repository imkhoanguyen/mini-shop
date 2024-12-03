import { Component, inject, Input, OnInit } from '@angular/core';
import { Order } from '../../../_models/order';
import { OrderService } from '../../../_services/order.service';
import { CommonModule } from '@angular/common';
import { UtilityService } from '../../../_services/utility.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from '../../../_services/toastr.service';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.css',
})
export class OrderDetailComponent implements OnInit {
  private orderService = inject(OrderService);
  utilService = inject(UtilityService);
  private route = inject(ActivatedRoute);
  private toastrService = inject(ToastrService);
  orderId: number = 0;
  orderDetail!: Order | null;

  ngOnInit(): void {
    this.orderId = +this.route.snapshot.paramMap.get('id')!;
    this.getOrderDetail();
  }

  getOrderDetail() {
    this.orderService.getOrderById(this.orderId).subscribe({
      next: (res) => {
        this.orderDetail = res;
        console.log(res);
      },
      error: (er) => console.log(er),
    });
  }

  calTotal(order: Order) {
    const discountPrice = Number(order.discountPrice) || 0;
    return Number(order.subTotal) + Number(order.shippingFee) - discountPrice;
  }

  confirmPayment() {
    this.orderService
      .updatePaymentStatus(this.orderId, this.utilService.PAYMENT_STATUS_PAID)
      .subscribe({
        next: (res) => {
          this.orderDetail = res;
          this.toastrService.success('Xác nhận thanh toán thành công');
        },
        error: (er) => {
          console.log(er);
        },
      });
  }

  confirmOrder() {
    console.log('click');
    this.orderService
      .updateOrderStatus(this.orderId, this.utilService.ORDER_STATUS_CONFIRMED)
      .subscribe({
        next: (res) => {
          this.orderDetail = res;
          this.toastrService.success('Xác nhận đơn hàng thành công');
        },
        error: (er) => {
          console.log(er);
        },
      });
  }
}
