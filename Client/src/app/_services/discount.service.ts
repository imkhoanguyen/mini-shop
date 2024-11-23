import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map,Observable } from "rxjs";
import { DiscountAdd,  DiscountDto,  DiscountUpdate } from "../_models/discount.module";
import { PaginatedResult } from "../_models/pagination";
import { HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class  DiscountService {
  constructor(private http: HttpClient) {}

  apiUrl = environment.apiUrl;

  getDiscountsPagedList(params: any): Observable<PaginatedResult< DiscountDto[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber) httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    if (params.pageSize) httpParams = httpParams.set('pageSize', params.pageSize.toString());
    if (params.orderBy) httpParams = httpParams.set('orderBy', params.orderBy);
    if (params.search) httpParams = httpParams.set('search', params.search);

    return this.http.get< DiscountDto[]>(this.apiUrl+"/Discount", {
      params: httpParams,
      observe: 'response'
    }).pipe(
      map((response: HttpResponse< DiscountDto[]>) => {
        const paginatedResult = new PaginatedResult< DiscountDto[]>();
        paginatedResult.items = response.body || [];

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }

  getAllDiscounts(): Observable< DiscountDto[]> {
    return this.http.get<DiscountDto[]>(`${this.apiUrl}/Discount/All`);
  }

  getDiscountById(id: number): Observable<DiscountDto> {
    return this.http.get<DiscountDto>(`${this.apiUrl}/Discount/${id}`);
  }

  addDiscount(discount: DiscountAdd): Observable<DiscountDto> {
    return this.http.post<DiscountDto>(`${this.apiUrl}/Discount/Add`, discount);
  }

  updateDiscount(discount: DiscountUpdate): Observable<DiscountDto> {
    return this.http.put<DiscountDto>(`${this.apiUrl}/Discount/Update`, discount);
  }

  deleteDiscount(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Discount/Delete?id=${id}`);
  }
}
