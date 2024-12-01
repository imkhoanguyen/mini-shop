import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../_services/order.service';
import { Order } from '../../../_models/order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.css',
})
export class CheckoutSuccessComponent implements OnInit {
  sessionId: string | null = null;
  private orderService = inject(OrderService);

  constructor(private route: ActivatedRoute) {}

  order: Order | undefined;

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.sessionId = params['session_id'];
    });

    if (this.sessionId) {
      this.getOrder();
    }
  }

  getOrder() {
    this.orderService.getOrderByStripeSessionId(this.sessionId!).subscribe({
      next: (res) => {
        this.order = res;
      },
      error: (er) => {
        console.log(er);
      },
    });
  }

  calFinal() {
    if (this.order) {
      const discountPrice = Number(this.order.discountPrice) || 0;

      return this.calTotal() - discountPrice;
    }
    return 0;
  }

  calTotal() {
    if (this.order) {
      const subTotal = Number(this.order.subTotal) || 0;
      const shippingFee = Number(this.order.shippingFee) || 0;

      return subTotal + shippingFee;
    }
    return 0;
  }
}
