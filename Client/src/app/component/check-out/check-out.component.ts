import { Component, OnInit } from '@angular/core';
import { Order, OrderItem, ShippingMethod } from '../../_models/types';
import { CheckoutService } from '../../_services/checkout.service';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrl: './check-out.component.css',
  standalone: true,
  imports: [FormsModule, DropdownModule],
  providers: [CheckoutService],
})
export class CheckOutComponent implements OnInit {
  shippingMethodId?: number;
  sms: ShippingMethod[] = [];

  order: Order = {
    subTotal: 0,
    orderDate: '',
    address: '',
    phone: '',
    shippingFee: '',
    shippingMethodId: 0,
    create: '',
    update: '',
    userId: '',
    discountId: 0,
    discountPrice: 0,
  };

  orderItem: OrderItem = {
    productId: 0,
    quantity: 0,
    price: 0,
    orderId: 0,
    productName: '',
    productColor: '',
    productSize: '',
  };

  constructor(private checkoutService: CheckoutService) {}

  ngOnInit() {
    this.checkoutService.getAllShippingMethod().subscribe({
      next: (res) => {
        this.sms = res;
        console.log(this.sms); // Ensure the data is populated correctly
      },
      error: (err) => {
        console.error('Failed to load shipping methods:', err);
      },
    });
  }

  onSubmit() {
    this.checkoutService.addOrder(this.order).subscribe({
      next: (res) => {
        console.log(res);
      },
    });
  }
}
