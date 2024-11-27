import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../_services/cart.service';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule } from '@angular/forms';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { CartItem } from '../../../_models/cart';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [InputNumberModule, FormsModule, BreadcrumbModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {
  cartService = inject(CartService);

  // breadcrumb
  items: MenuItem[] | undefined;

  ngOnInit(): void {
    this.items = [
      { label: 'Trang chủ', routerLink: '/home' },
      { label: 'Giỏ hàng', routerLink: '/cart' },
    ];
  }

  onPlus(item: CartItem) {
    this.cartService.addItemToCart(item);
  }

  onMinus(item: CartItem) {
    this.cartService.removeItemFromCart(item);
  }

  onRemoveItem(item: CartItem) {
    this.cartService.removeItemFromCart(item, item.quantity);
  }
}
