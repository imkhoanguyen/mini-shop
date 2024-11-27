import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Cart, CartItem } from '../_models/cart';
import { firstValueFrom, map, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);

  getCart(id: string) {
    return this.http.get<Cart>(this.baseUrl + '/cart?id=' + id).pipe(
      map((cart) => {
        this.cart.set(cart);
        return cart;
      })
    );
  }

  private createCart(): Cart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }

  private addOrUpdateItem(
    items: CartItem[],
    item: CartItem,
    quantity: number
  ): CartItem[] {
    const index = items.findIndex((x) => x.variantId === item.variantId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  async addItemToCart(item: CartItem, quantity = 1): Promise<boolean> {
    try {
      const cart = this.cart() ?? this.createCart();
      cart.items = this.addOrUpdateItem(cart.items, item, quantity);
      await firstValueFrom(this.setCart(cart));
      return true;
    } catch (error) {
      console.error('Failed to add item to cart:', error);
      return false;
    }
  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + '/cart', cart).pipe(
      tap((cart) => {
        this.cart.set(cart);
      })
    );
  }
}
