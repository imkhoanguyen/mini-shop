import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { OrderAdd, Order } from '../_models/order';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}
  apiUrl = environment.apiUrl;

  addOrder(order: OrderAdd) {
    return this.http.post<OrderAdd>(this.apiUrl + '/order', order);
  }

  getOrderByStripeSessionId(stripeSessionId: string) {
    return this.http.get<Order>(
      `${this.apiUrl}/Order/stripeSessionId/${stripeSessionId}`
    );
  }

  getOrderByUserId(id: string): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/Order/${id}`);
  }
  getRevenueOrderToday() {
    return this.http.get<any>(`${this.apiUrl}/Order/revenue/today`);
  }
  getRevenueOrderDate(Date: string) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/revenue/by-date?date=${Date}`
    );
  }
  getRevenueOrderMonth(year: number, month: number) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/revenue/monthly?year=${year}&month=${month}`
    );
  }
  getRevenueOrderYear(year: number) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/revenue/yearly?year=${year}`
    );
  }
  getRevenueOrderAllMonthInYear(year: number) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/revenue/allMonthInYear?year=${year}`
    );
  }

  getCountOrderToday() {
    return this.http.get<any>(`${this.apiUrl}/Order/count-today`);
  }
  getCountOrderDate(Date: string) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/count-orders/by-date?date=${Date}`
    );
  }
  getCountOrderMonth(year: number, month: number) {
    return this.http.get<any>(
      `${this.apiUrl}/Order/count-monthly?year=${year}&month=${month}`
    );
  }
  getCountOrderYear(year: number) {
    return this.http.get<any>(`${this.apiUrl}/Order/count-yearly?year=${year}`);
  }
}
