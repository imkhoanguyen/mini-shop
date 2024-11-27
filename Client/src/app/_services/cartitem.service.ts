import { HttpBackend, HttpClient } from "@angular/common/http";
import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { CartItem } from "../_models/cartitem.module";
import { Observable } from "rxjs";

@Injectable(
{
    providedIn: 'root'
})
export class CartItemsService{
    constructor(private http: HttpClient) {}
   cartItemsList=signal<CartItem[]>([]);
   cartItemsItems=signal<CartItem>({
    id: 0,
    quantity:0,
    price:0
   });
   apiUrl=environment.apiUrl;

   addCartItem(data: CartItem):Observable<CartItem>{
    return this.http.post<CartItem>(this.apiUrl+"/CartItem/Add",data);
   }
   updateCartItem(data: CartItem):Observable<CartItem>{
    return this.http.put<CartItem>(this.apiUrl+"/CartItem/Update",data);
   }
   deleteCartItem(data:CartItem):Observable<CartItem>{
    return this.http.delete<CartItem>(this.apiUrl+"/CartItem/Delete",{body:data});
   }
   getAllCartItem(){
    return this.http.get<CartItem>(this.apiUrl+"/CartItem/GetAll");
   }
}
