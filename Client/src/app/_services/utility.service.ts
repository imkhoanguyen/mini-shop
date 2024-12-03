import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  public readonly PAYMENT_OFFILINE = 'Offline';
  public readonly PAYMENT_ONLINE = 'Online';
  public readonly ORDER_STATUS_UNCONFIRMED = 'Unconfirmed';
  public readonly ORDER_STATUS_CONFIRMED = 'Confirmed';
  public readonly ORDER_STATUS_CANCELED = 'Canceled';
  public readonly PAYMENT_STATUS_PENDING = 'Pending';
  public readonly PAYMENT_STATUS_PAID = 'Paid';

  public readonly LIST_PAYMENT_METHOD = [
    this.PAYMENT_OFFILINE,
    this.PAYMENT_ONLINE,
  ];
  public readonly LIST_ORDER_STATUS = [
    this.ORDER_STATUS_CANCELED,
    this.ORDER_STATUS_UNCONFIRMED,
    this.ORDER_STATUS_CONFIRMED,
  ];
  public readonly LIST_PAYMENT_STATUS = [
    this.PAYMENT_STATUS_PENDING,
    this.PAYMENT_STATUS_PAID,
  ];

  // format date vietnam
  private readonly defaultDate = '0001-01-01T00:00:00';
  getFormattedDate(date: string | null): string {
    if (!date || date === this.defaultDate) {
      return 'Chưa cập nhật';
    }

    const options: Intl.DateTimeFormatOptions = {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false,
    };

    return new Date(date).toLocaleString('vi-VN', options);
  }
}
