import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Product, ProductAdd } from "../_models/product.module";
import { Observable } from "rxjs";

@Injectable(
{
  providedIn: 'root'
})

export class ProductService {
  constructor(private http: HttpClient){}
  apiUrl = environment.apiUrl;

  productList = signal<Product[]>([]);
  productItems = signal<Product>({
    id: 0,
  name: "",
  description: "",
  created: new Date(),
  updated: new Date(),
  variants: [],
  categoryIds: [],
  imageUrls: [],
  status: 0,
  });
  addProduct(data: ProductAdd): Observable<any>{
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
  getAllProduct(){
    return this.http.get<Product[]>(this.apiUrl + "/Product/GetAll");
  }
  getProductAllPaging(){
    return this.http.get<Product[]>(this.apiUrl + "/Product/GetAllPaging");
  }
}

