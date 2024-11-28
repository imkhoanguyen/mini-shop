import { Component, inject, OnInit } from '@angular/core';
import { CartService } from '../../../_services/cart.service';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule } from '@angular/forms';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { CartItem } from '../../../_models/cart';
import { ImageModule } from 'primeng/image';
import { Router } from '@angular/router';
import { AccountService } from '../../../_services/account.service';
import { ToastrService } from '../../../_services/toastr.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [InputNumberModule, FormsModule, BreadcrumbModule, ImageModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css',
})
export class CartComponent implements OnInit {
  cartService = inject(CartService);
  private router = inject(Router);
  private accountService = inject(AccountService);
  private toastrService = inject(ToastrService);

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

  checkout() {
    if (this.cartService.itemCount() <= 0) {
      this.toastrService.error('Giỏ hàng đang trống');
      return;
    }

    if (this.accountService.currentUser()) {
      this.router.navigateByUrl('/cart/checkout');
    } else {
      this.router.navigateByUrl('/login');
    }
  }
}
