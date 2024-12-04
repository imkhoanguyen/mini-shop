import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { HttpResponse } from '@angular/common/http';
import {
  ProductAdd,
  ProductDto,
  ProductUpdate,
} from '../_models/product.module';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  apiUrl = environment.apiUrl;

  getProductsPagedList(
    params: any,
    tracked: boolean
  ): Observable<PaginatedResult<ProductDto[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber)
      httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    if (params.pageSize)
      httpParams = httpParams.set('pageSize', params.pageSize.toString());
    if (params.orderBy) httpParams = httpParams.set('orderBy', params.orderBy);
    if (params.search) httpParams = httpParams.set('search', params.search);
    if (params.selectedColor)
      httpParams = httpParams.set('selectedColor', params.selectedColor);
    if (params.selectedSize && params.selectedSize.length > 0) {
      params.selectedSize.forEach((size: string) => {
        httpParams = httpParams.append('selectedSize', size);
      });
    }
    if (params.selectedCategory && params.selectedCategory.length > 0) {
      params.selectedCategory.forEach((category: string) => {
        httpParams = httpParams.append('selectedCategory', category);
      });
    }

    return this.http
      .get<ProductDto[]>(this.apiUrl + '/Product/?tracked=' + tracked, {
        params: httpParams,
        observe: 'response',
      })
      .pipe(
        map((response: HttpResponse<ProductDto[]>) => {
          const paginatedResult = new PaginatedResult<ProductDto[]>();
          paginatedResult.items = response.body || [];

          const paginationHeader = response.headers.get('Pagination');
          if (paginationHeader) {
            paginatedResult.pagination = JSON.parse(paginationHeader);
          }

          return paginatedResult;
        })
      );
  }
  getAllProducts() {
    return this.http.get<ProductDto[]>(`${this.apiUrl}/Product/all`);
  }
  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/Product/${id}`);
  }
  getAllProductByCategory(id: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Product/categoryId/${id}`);
  }

  addProduct(Product: FormData): Observable<ProductDto> {
    return this.http.post<ProductDto>(this.apiUrl + '/Product/Add', Product);
  }
  updateProduct(Product: FormData): Observable<ProductDto> {
    return this.http.put<ProductDto>(`${this.apiUrl}/Product/Update`, Product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Product/Delete?id=${id}`);
  }
  addProductImage(productId: number, frmData: FormData) {
    return this.http.post(
      `${this.apiUrl}/product/add-images/${productId}`,
      frmData
    );
  }
  removeProductImage(productId: number, imageId: number) {
    return this.http.delete(
      `${this.apiUrl}/product/remove-image/${productId}?imageId=${imageId}`
    );
  }

  revertQuantityProduct(orderId: number) {
    return this.http.put(
      this.apiUrl + `/product/revert-quantity/${orderId}`,
      {}
    );
  }
}
