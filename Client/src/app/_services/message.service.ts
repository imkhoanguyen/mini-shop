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

  getMessages(senderId: string, recipientId: string, skip: number, take: number): Observable<MessageDto[]> {
    return this.http.get<MessageDto[]>(this.apiUrl +
      "/Messages/GetMessageThread?senderId=" + senderId +
      "&recipientId=" + recipientId +
      "&skip="+skip+
      "&take="+take);
  }
  uploadFiles(files: FormData): Observable<{ fileUrl: string, fileType: string }[]> {
    return this.http.post<{ fileUrl: string, fileType: string }[]>(this.apiUrl + "/Messages/UploadFiles", files);
  }

  addMessage(data: MessageAdd): Observable<MessageAdd> {
    return this.http.post<MessageAdd>(this.apiUrl + "/Messages/AddMessage", data);
  }
  getLastMessage(senderId: string, recipientId: string): Observable<MessageDto> {
    return this.http.get<MessageDto>(this.apiUrl + "/Messages/GetLastMessage?senderId=" + senderId + "&recipientId=" + recipientId);
  }
}
