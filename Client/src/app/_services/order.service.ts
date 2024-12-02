import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { OrderAdd, Order, OrderParams } from '../_models/order';
import { map, Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}
  apiUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Order[]> = new PaginatedResult<Order[]>();

  GetAllWithLimit(prm: OrderParams) {
    let params = new HttpParams();
    if (prm.pageSize && prm.pageNumber) {
      params = params.append('pageNumber', prm.pageNumber);
      params = params.append('pageSize', prm.pageSize);
    }

    if (prm.search) params = params.append('search', prm.search);
    if (prm.orderBy) params = params.append('orderBy', prm.orderBy);
    if (prm.selectedPaymentStatus)
      params = params.append(
        'selectedPaymentStatus',
        prm.selectedPaymentStatus
      );
    if (prm.selectedStatus)
      params = params.append('selectedStatus', prm.selectedStatus);
    if (prm.startDate && prm.endDate) {
      params = params.append('startDate', prm.startDate);
      params = params.append('endDate', prm.endDate);
    }

    if (prm.userId) params = params.append('userId', prm.userId);

    return this.http
      .get<Order[]>(this.apiUrl + '/order', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) {
            this.paginatedResult.items = response.body;
          }
          const pagination = response.headers.get('Pagination');
          if (pagination) {
            this.paginatedResult.pagination = JSON.parse(pagination);
          }
          return this.paginatedResult;
        })
      );
  }

  addOrder(order: OrderAdd) {
    return this.http.post<OrderAdd>(this.apiUrl + '/order', order);
  }

  deleteOrder(orderId: number) {
    return this.http.delete(this.apiUrl + `/order/${orderId}`);
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
