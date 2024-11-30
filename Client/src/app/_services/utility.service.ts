import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  public readonly PAYMENT_OFFILINE = 'Offline';
  public readonly PAYMENT_ONLINE = 'Online';
}
