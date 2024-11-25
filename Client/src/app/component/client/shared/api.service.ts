import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

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
 
 removeToCart (data :ProductGet){
this.cartItemList.map ( (a:ProductGet ,index :ProductGet)=>
{
    if(data.id === a.id)
    {
        this.cartItemList.splice(index,1)
    }
})
this.productList.next(this.cartItemList)
}
 products ()
 {
    return this.productList.asObservable();
 }

}