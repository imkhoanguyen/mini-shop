import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { MessageDto } from '../_models/message.module';
import { BehaviorSubject, Subscription, timer } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ChatService implements OnDestroy {
  private hubConnection: signalR.HubConnection;
  private reconnectSubscription: Subscription = new Subscription();

  // State management
  public typingBlocked$ = new BehaviorSubject<string | null>(null);
  public typingStatus$ = new BehaviorSubject<{ isTyping: boolean; adminId: string } | null>(null);
  public messageReceived$ = new BehaviorSubject<MessageDto | null>(null);
  public connectionState$ = new BehaviorSubject<string>('disconnected');

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.hubUrl)
      .withAutomaticReconnect() // Tự động thử kết nối lại
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.registerHubEvents();
    this.startConnection();
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.connectionState$.next('connected');
      })
      .catch(err => {
        console.error('Error starting connection:', err);
        this.connectionState$.next('disconnected');
        this.startReconnect();
      });
  }
  private startReconnect(): void {
    this.connectionState$.next('reconnecting');
    this.reconnectSubscription = timer(5000)
      .pipe(
        switchMap(() =>
          this.hubConnection
            .start()
            .then(() => {
              console.log('Reconnected');
              this.connectionState$.next('connected');
              return null;
            })
            .catch(err => {
              console.error('Reconnection attempt failed:', err);
              return null;
            })
        )
      )
      .subscribe();
  }

  private registerHubEvents(): void {
    this.hubConnection.on('ReceiveMessage', (message: MessageDto) => {
      this.messageReceived$.next(message);
    });

    this.hubConnection.on('ReceiveTypingStatus', (isTyping: boolean, adminId: string) => {
      this.typingStatus$.next({ isTyping, adminId });
    });

    this.hubConnection.on('TypingBlocked', (adminId: string) => {
      this.typingBlocked$.next(adminId);
    });

    this.hubConnection.onclose(() => {
      console.warn('Connection closed. Attempting to reconnect...');
      this.connectionState$.next('disconnected');
      this.startReconnect();
    });
  }


  joinGroup(recipientId: string, senderId: string) {
    this.hubConnection.invoke('JoinGroup', recipientId, senderId).catch(err => console.error('Error joining group:', err));
  }

  notifyTyping(recipientId: string, senderId: string) {
    this.hubConnection.invoke('NotifyTyping', recipientId, senderId).catch(err => console.error('Error notifying typing:', err));
  }

  stopTyping(recipientId: string, senderId: string) {
    this.hubConnection.invoke('StopTyping', recipientId, senderId).catch(err => console.error('Error stopping typing:', err));
  }

  sendMessage(message: MessageDto): void {
    this.hubConnection
      .invoke('SendMessage', message)
      .catch(err => console.error('Error sending message:', err));
  }

  ngOnDestroy(): void {
    if (this.reconnectSubscription) {
      this.reconnectSubscription.unsubscribe();
    }
    this.hubConnection
      .stop()
      .then(() => console.log('SignalR connection stopped'))
      .catch(err => console.error('Error stopping SignalR connection:', err));
  }
}
