import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Category } from "../_models/category.module";
import { Observable, tap } from "rxjs";
import { Pagination } from "../_models/pagination.module";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient){}

  apiUrl = environment.apiUrl;

  categoryList = signal<Category[]>([]);
  categoryItems = signal<Category>({
    id: 0,
    name: "",
    created: new Date(),
    updated: new Date(),
  });

  addCategory(data: Category): Observable<Category>{
    return this.http.post<Category>(this.apiUrl + "/Category/Add", data);
  }

  updateCategory(data: Category): Observable<any>{
    return this.http.put(this.apiUrl + "/Category/Update", data);
  }

  deleteCategory(data: Category): Observable<any>{
    return this.http.delete(this.apiUrl + "/Category/Delete/", { body: data });
  }
   getCategoryById(id: number){
    return this.http.get<Category>(this.apiUrl + "/Category/GetById/" + id)
      .subscribe((data) => {
        this.categoryItems.set(data);
      });
  }

  getAllCategories(){
    return this.http.get<Category[]>(this.apiUrl + "/Category/GetAll")
  }
  getCategoryNameById(categoryId: number){
    return this.http.get(this.apiUrl + "/Category/GetCategoryNameById/" + categoryId);
  }

  getCategoriesAllPaging(pageNumber: number, pageSize: number, searchString?: string): Observable<Pagination<Category>>{
    let param = new HttpParams()
      .set("pageNumber", pageNumber.toString())
      .set("pageSize", pageSize.toString());

    if(searchString){
      param = param.set("searchString", searchString);
    }
    return this.http.get<Pagination<Category>>(this.apiUrl + "/Category/GetAllPaging", { params: param });
  }
}
