import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { OrderService } from '../../../_services/order.service';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-checkout-cancel',
  standalone: true,
  imports: [CommonModule, RouterLink, ButtonModule],
  templateUrl: './checkout-cancel.component.html',
  styleUrl: './checkout-cancel.component.css',
})
export class CheckoutCancelComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}
  private orderService = inject(OrderService);
  private productService = inject(ProductService);

  orderId: number = 0;
  flag = false;

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.orderId = params['order_id'];
    });

    if (this.orderId > 0) {
      this.productService.revertQuantityProduct(this.orderId).subscribe({
        next: () => {
          this.deleteOrder();
        },
        error: (error) => {
          console.error('Error occurred:', error);
        },
      });
    }
  }

  deleteOrder() {
    this.orderService.deleteOrder(this.orderId).subscribe({
      next: (_) => {
        this.flag = true;
      },
      error: (er) => {
        console.log(er);
      },
    });
  }
}
