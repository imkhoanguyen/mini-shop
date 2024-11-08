import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Order, ShippingMethod } from '../_models/types';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  constructor(private http: HttpClient) {}
  apiUrl = environment.apiUrl;

  addOrder(data: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl + '/Order/Add', data);
  }

  getAllShippingMethod(): Observable<ShippingMethod[]> {
    return this.http.get<ShippingMethod[]>(
      this.apiUrl + '/ShippingMethod/GetAll'
    );
  }
}
