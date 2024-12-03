import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { OrderService } from '../../../_services/order.service';
import { UtilityService } from '../../../_services/utility.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from '../../../_services/toastr.service';
import { Order } from '../../../_models/order';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-order-detail-client',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-detail-client.component.html',
  styleUrl: './order-detail-client.component.css',
})
export class OrderDetailClientComponent {
  private orderService = inject(OrderService);
  utilService = inject(UtilityService);
  private route = inject(ActivatedRoute);
  private toastrService = inject(ToastrService);
  private productService = inject(ProductService);
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

  cancelOrder() {
    console.log('click');
    this.orderService
      .updateOrderStatus(this.orderId, this.utilService.ORDER_STATUS_CANCELED)
      .subscribe({
        next: (res) => {
          this.orderDetail = res;
          this.toastrService.success('Hủy đơn hàng thành công');
          this.revertQuantityProduct();
        },
        error: (er) => {
          console.log(er);
        },
      });
  }

  revertQuantityProduct() {
    this.productService
      .revertQuantityProduct(this.orderDetail?.id || 0)
      .subscribe({
        next: (_) => {
          console.log('revert-quantity-success');
        },
        error: (er) => {
          console.log(er);
        },
      });
  }
}
