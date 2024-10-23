import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Message } from '../_models/message.module';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getMessages(senderId: string, recipientId: string, skip: number, take: number): Observable<Message[]> {
    return this.http.get<Message[]>(this.apiUrl +
      "/Messages/GetMessageThread?senderId=" + senderId +
      "&recipientId=" + recipientId +
      "&skip="+skip+
      "&take="+take);
  }

  sendMessage(data: Message): Observable<Message> {
    return this.http.post<Message>(this.apiUrl + "/Messages/SendMessage", data);
  }
  getLastMessage(senderId: string, recipientId: string): Observable<Message> {
    return this.http.get<Message>(this.apiUrl + "/Messages/GetLastMessage?senderId=" + senderId + "&recipientId=" + recipientId);
  }
}
