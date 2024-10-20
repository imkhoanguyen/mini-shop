import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Product, ProductAdd, ProductUpdate } from "../_models/product.module";
import { Observable } from "rxjs";
import { Pagination } from "../_models/pagination.module";

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
  status: 0,
  });
  addProduct(data: ProductAdd): Observable<any>{
    return this.http.post(this.apiUrl + "/Product/Add", data);
  }
  updateProduct(data: ProductUpdate): Observable<any>{
    return this.http.put(this.apiUrl + "/Product/Update", data);
  }
  deleteProduct(data: Product): Observable<any>{
    return this.http.delete(this.apiUrl + "/Product/Delete/", { body: data });
  }
  getProductById(id: number){
    return this.http.get<Product>(this.apiUrl + "/Product/GetProductById" + id);
  }
  getAllProduct(){
    return this.http.get<Product[]>(this.apiUrl + "/Product/GetAll");
  }
  getProductAllPaging(pageNumber: number, pageSize: number, searchString?: string): Observable<Pagination<Product>>{
    let param = new HttpParams()
      .set("pageNumber", pageNumber.toString())
      .set("pageSize", pageSize.toString());

    if(searchString){
      param = param.set("searchString", searchString);
    }

    return this.http.get<Pagination<Product>>(this.apiUrl + "/Product/GetAllPaging", { params: param });
  }
}

