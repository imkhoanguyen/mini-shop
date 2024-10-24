import { HttpClient } from "@angular/common/http";
import { signal } from "@angular/core";

export class ShippingMethod{
    constructor(private http: HttpClient){}

    shippingMethodList=signal<ShippingMethod[]>([]);
}