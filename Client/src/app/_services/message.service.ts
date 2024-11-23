import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageAdd, MessageDto } from '../_models/message.module';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getMessages(customerId: string): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(this.apiUrl +
      "/Messages/GetMessageThread?customerId=" + customerId);
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
