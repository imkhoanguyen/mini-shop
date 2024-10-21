import { HttpClient } from "@angular/common/http";
import { Injectable, signal } from "@angular/core";
import { ShoppingCart } from "../_models/shoppingcart.module";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
    providedIn:'root'
})
export class ShoppingService{
    constructor(private http: HttpClient){}
    shoppingCartList=signal<ShoppingCart[]>([]);
    shoppingCartItems=signal<ShoppingCart>({
        id:0,
        created:new Date,
        updated:new Date,
        cartItems: [],
    })
    apiUrl=environment.apiUrl;
    addShoppingCart(data: ShoppingCart):Observable <ShoppingCart>{
        return this.http.post<ShoppingCart>(this.apiUrl+"/ShoppingCart/Add",data);
    }
    updateShoppingCart(data:ShoppingCart):Observable <any>{
        return this.http.put(this.apiUrl+"/ShoppingCart/Update",data);
    }
    deleteShoppingCart(data:ShoppingCart):Observable<any>{
        return this.http.delete(this.apiUrl+"/ShoppingCart/Delete",{body:data});
    }
    getAllShoppingCart(data:ShoppingCart){
        return this.http.get(this.apiUrl+"/ShoppingCart/GetAll");
    }
}