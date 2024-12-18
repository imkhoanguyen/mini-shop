import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { OrderAdd } from '../_models/order';
import { Order } from '../_models/types';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  createSessionCheckout(order: OrderAdd, cartId: string) {
    return this.http.post<string>(this.baseUrl + '/payment', { order, cartId });
  }
}
