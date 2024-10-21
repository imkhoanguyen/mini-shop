import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable ( {
    providedIn : 'root'
})

export  class   productUserService {
constructor (private http : HttpClient){}

getAllProduct ( ) : Observable<any[]>{
    return this.http.get<any[]>("http://localhost:3000/products/GetAllProducts");
}

getAllProductByCategory (id  : number ) : Observable<any[]>{
      return this.http.get<any[]>(`http://localhost:3000/products/GetAllProductsByCategory?id=${id}`);
}
}