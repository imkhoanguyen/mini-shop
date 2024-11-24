import { Injectable } from "@angular/core";
import { HttpClient } from "@microsoft/signalr";
import { BehaviorSubject } from "rxjs";
import { ProductGet } from "../../../_models/product.module";
@Injectable({
    providedIn:'root'
})
export class ApiService
{
 public cartItemList : any=[];
 public  productList =  new BehaviorSubject<any>([])
 constructor (private http:HttpClient){}

 addToCart(data :ProductGet)
 { 
    this.cartItemList.push(data) ;
    this.productList.next(this.cartItemList);
    console.log("add to cart" ,this.cartItemList)
 }
 
 products ()
 {
    return this.productList.asObservable();
 }

}