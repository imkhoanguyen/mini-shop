import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Category } from "../models/category.module";
import { Product } from "../models/product.module";

@Injectable(
{
  providedIn: 'root'
})

export class ProductService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addProduct(data: Product){
    return this.http.post(this.apiUrl + "/Product/Add", data);
  }
  updateProduct(data: Product){
    return this.http.put(this.apiUrl + "/Product/Update", data);
  }
  deleteProduct(id: number){
    return this.http.delete(this.apiUrl + "/Product/Delete/" + id);
  }
  getProductById(id: number){
    return this.http.get<Product>(this.apiUrl + "/Product/GetById/" + id);
  }
  getAllCategories(){
    return this.http.get<Product[]>(this.apiUrl + "/Product/GetAll");
  }
  getProductAllPaging(){
    return this.http.get<Product[]>(this.apiUrl + "/Product/GetAllPaging");
  }
}

