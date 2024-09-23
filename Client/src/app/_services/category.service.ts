import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Category } from "../_models/category.module";
import { Observable } from "rxjs";

@Injectable(
{
  providedIn: 'root'
})

export class CategoryService {
  constructor(private http: HttpClient){}


  apiUrl = environment.apiUrl;

  categoryList = signal<Category[]>([]);
  categoryItems = signal<Category>({
    id: 0,
    name: ""
  });


  addCategory(data: Category): Observable<Category>{
    return this.http.post<Category>(this.apiUrl + "/Category/Add", data);
  }
  updateCategory(data: Category): Observable<any>{
    return this.http.put(this.apiUrl + "/Category/Update", data);
  }
  deleteCategory(id: number): Observable<any>{
    return this.http.delete(this.apiUrl + "/Category/Delete/" + id);
  }
  getCategoryById(id: number){
    return this.http.get<Category>(this.apiUrl + "/Category/GetById/" + id)
      .subscribe((data) => {
        this.categoryItems.set(data);
      });
  }
  getAllCategories(){
    return this.http.get<Category[]>(this.apiUrl + "/Category/GetAll")
      .subscribe((data) => {
        this.categoryList.set(data);
      });
  }
  getCategoryAllPaging(){
    return this.http.get<Category[]>(this.apiUrl + "/Category/GetAllPaging")
      .subscribe((data) => {
        this.categoryList.set(data);
      });;
  }
}

