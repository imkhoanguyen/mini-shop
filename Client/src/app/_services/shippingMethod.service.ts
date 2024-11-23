import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map,Observable } from "rxjs";
import { ShippingMethodAdd, ShippingMethodDto, ShippingMethodUpdate } from "../_models/shippingMethod.module";
import { PaginatedResult } from "../_models/pagination";
import { HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ShippingMethodService {
  constructor(private http: HttpClient) {}

  apiUrl = environment.apiUrl;

  getShippingMethodsPagedList(params: any): Observable<PaginatedResult<ShippingMethodDto[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber) httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    if (params.pageSize) httpParams = httpParams.set('pageSize', params.pageSize.toString());
    if (params.orderBy) httpParams = httpParams.set('orderBy', params.orderBy);
    if (params.search) httpParams = httpParams.set('search', params.search);

    return this.http.get<ShippingMethodDto[]>(this.apiUrl+"/ShippingMethod", {
      params: httpParams,
      observe: 'response'
    }).pipe(
      map((response: HttpResponse<ShippingMethodDto[]>) => {
        const paginatedResult = new PaginatedResult<ShippingMethodDto[]>();
        paginatedResult.items = response.body || [];

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }

  getAllShippingMethods(): Observable<ShippingMethodDto[]> {
    return this.http.get<ShippingMethodDto[]>(`${this.apiUrl}/ShippingMethod/All`);
  }

  getShippingMethodById(id: number): Observable<ShippingMethodDto> {
    return this.http.get<ShippingMethodDto>(`${this.apiUrl}/ShippingMethod/${id}`);
  }

  addShippingMethod(shippingMethod: ShippingMethodAdd): Observable<ShippingMethodDto> {
    return this.http.post<ShippingMethodDto>(`${this.apiUrl}/ShippingMethod/Add`, shippingMethod);
  }

  updateShippingMethod(shippingMethod: ShippingMethodUpdate): Observable<ShippingMethodDto> {
    return this.http.put<ShippingMethodDto>(`${this.apiUrl}/ShippingMethod/Update`, shippingMethod);
  }

  deleteShippingMethod(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/ShippingMethod/Delete?id=${id}`);
  }
}
