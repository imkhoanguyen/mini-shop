import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable ( {
    providedIn : 'root'
})

export  class   productUserService {
constructor (private http : HttpClient){}

getAllProduct ( ) : Observable<any[]>{
    console.log ("tesk1")
    return this.http.get<any[]>("http://localhost:3000/products/GetAllProducts");
}
}