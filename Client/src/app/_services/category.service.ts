import { Injectable, signal } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { CategoryAdd, CategoryDto, CategoryUpdate } from "../_models/category.module";
import { PaginatedResult } from "../_models/pagination";
import { HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient){}

  apiUrl = environment.apiUrl;

  getCategoriesPagedList(params: any): Observable<PaginatedResult<CategoryDto[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber) httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    if (params.pageSize) httpParams = httpParams.set('pageSize', params.pageSize.toString());
    if (params.orderBy) httpParams = httpParams.set('orderBy', params.orderBy);
    if (params.search) httpParams = httpParams.set('search', params.search);

    return this.http.get<CategoryDto[]>(this.apiUrl + "/Category", {
      params: httpParams,
      observe: 'response'
    }).pipe(
      map((response: HttpResponse<CategoryDto[]>) => {
        const paginatedResult = new PaginatedResult<CategoryDto[]>();
        paginatedResult.items = response.body || [];

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }
  getAllCategories () {
    return this.http.get<CategoryDto[]>(`${this.apiUrl}/Category/all`);
  }
  getCategoryById(id: number): Observable<CategoryDto> {
    return this.http.get<CategoryDto>(`${this.apiUrl}/Category/${id}`);
  }
  addCategory(category: CategoryAdd): Observable<CategoryDto> {
    return this.http.post<CategoryDto>(this.apiUrl+"/Category/Add", category);
  }
  updateCategory(category: CategoryUpdate): Observable<CategoryDto> {
    return this.http.put<CategoryDto>(`${this.apiUrl}/Category/Update`, category);
  }

  deleteCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Category/Delete?id=${id}`);
  }
}
