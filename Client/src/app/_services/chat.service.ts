import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { MessageDto } from '../_models/message.module';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection: signalR.HubConnection;
  hubUrl = environment.hubUrl;
  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .build();
    this.startConnection();
  }

  private startConnection() {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public sendMessage(message: MessageDto) {
    this.hubConnection.invoke('SendMessage', message);
  }

  public onMessageReceived(callback: (message: MessageDto) => void) {
    this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
      callback(message);
    });
  }

}
