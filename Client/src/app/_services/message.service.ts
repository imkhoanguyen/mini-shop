import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { MessageAdd, MessageDto } from '../_models/message.module';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getMessageThread(params: any, customerId: string): Observable<PaginatedResult<MessageDto[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber) {
      httpParams = httpParams.set('PageNumber', params.pageNumber.toString());
    }
    if (params.pageSize) {
      httpParams = httpParams.set('PageSize', params.pageSize.toString());
    }
    if (params.search) {
      httpParams = httpParams.set('Search', params.search);
    }
    return this.http.get<MessageDto[]>(`${this.apiUrl}/Messages/GetMessageThread?customerId=${customerId}`, {
      params: httpParams,
      observe: 'response'
    }).pipe(
      map((response: HttpResponse<MessageDto[]>) => {
        const paginatedResult = new PaginatedResult<MessageDto[]>();
        paginatedResult.items = response.body || [];

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }
  addMessage(data: FormData){
    return this.http.post<MessageDto>(this.apiUrl + "/Messages/AddMessage", data);
  }
  getLastMessage(userId: string): Observable<MessageDto> {
    return this.http.get<MessageDto>(this.apiUrl + "/Messages/GetLastMessage?userId=" + userId);
  }
  getCustomers() {
    return this.http.get<string[]>(this.apiUrl + "/Messages/GetCustomers");
  }
}
