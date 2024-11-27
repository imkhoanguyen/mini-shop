import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { MainComponent } from './component/admin/main/main.component';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './component/admin/sidebar/sidebar.component';
import { ReviewComponent } from './component/review/review.component';
import { ToastModule } from 'primeng/toast';
import { CartService } from './_services/cart.service';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    CommonModule,
    SidebarComponent,
    MainComponent,
    ReviewComponent,
    ToastModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Client';
  constructor(private router: Router) {}
  private cartService = inject(CartService);

  ngOnInit(): void {
    this.setCurrentCart();
  }

  setCurrentCart() {
    const cartId = localStorage.getItem('cart_id');
    if (cartId) {
      this.cartService.getCart(cartId).subscribe({
        next: (cart) => {
          this.cartService.cart.set(cart);
          console.log(cart);
        },
        error: (err) => {
          console.error('Failed to load cart:', err);
        },
      });
    }
  }

  isAdminPage(): boolean {
    return this.router.url.startsWith('/admin');
  }
}
