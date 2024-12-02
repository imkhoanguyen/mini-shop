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
    this,
    this.ORDER_STATUS_UNCONFIRMED,
    this.ORDER_STATUS_CONFIRMED,
  ];
  public readonly LIST_PAYMENT_STATUS = [
    this.PAYMENT_STATUS_PENDING,
    this.PAYMENT_STATUS_PAID,
  ];
}
