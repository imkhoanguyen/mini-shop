import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { CartService } from './_services/cart.service';
import { FooterClientComponent } from './layout/footerClient/footerClient.component';
import { LoadingService } from './_services/loading.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    CommonModule,
    ToastModule,
    FooterClientComponent,
    ProgressSpinnerModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Client';
  constructor(private router: Router) {}
  private cartService = inject(CartService);
  private cdr = inject(ChangeDetectorRef);
  isLoading = false;
  private loadingService = inject(LoadingService);

  ngOnInit(): void {
    this.loadingService.loading$.subscribe((loading) => {
      this.isLoading = loading;
      this.cdr.detectChanges();
    });
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
