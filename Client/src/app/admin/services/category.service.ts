import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Category } from "../models/category.module";

@Injectable(
{
  providedIn: 'root'
})

export class CategoryService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addCategory(data: Category){
    return this.http.post(this.apiUrl + "/Category/Add", data);
  }
  updateCategory(data: Category){
    return this.http.put(this.apiUrl + "/Category/Update", data);
  }
  deleteCategory(id: number){
    return this.http.delete(this.apiUrl + "/Category/Delete/" + id);
  }
  getCategoryById(id: number){
    return this.http.get<Category>(this.apiUrl + "/Category/GetById/" + id);
  }
  getAllCategories(){
    return this.http.get<Category[]>(this.apiUrl + "/Category/GetAll");
  }
  getCategoryAllPaging(){
    return this.http.get<Category[]>(this.apiUrl + "/Category/GetAllPaging");
  }
}

