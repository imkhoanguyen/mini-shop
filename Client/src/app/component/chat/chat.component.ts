import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { User } from '../../_models/user.module';
import { AccountService } from '../../_services/account.service';
import { UserService } from '../../_services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from '../../_services/message.service';
import { Message } from '../../_models/message.module';
import { ChatService } from '../../_services/chat.service';
import { ChangeDetectorRef, NgZone } from '@angular/core';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  showChatWindow = true;
  adminUsers: User[] = [];
  messages: Message[] = [];
  user!: User;
  content: string = '';
  private chatService = inject(ChatService);
  private accountService = inject(AccountService);
  private userService = inject(UserService);
  private messageService = inject(MessageService);
  constructor(private cd: ChangeDetectorRef, private zone: NgZone) {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      this.user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(this.user);
    }
  }
  ngOnInit() {
    this.loadUsersWithAdminRole();
    this.chatService.onMessageReceived((content) => {
      this.messages.push(content);
    });
  }

  toggleChatWindow() {
    this.showChatWindow = !this.showChatWindow;
  }
  loadUsersWithAdminRole() {
    this.userService.getUsersWithAdminRole().subscribe((users) => {
      this.adminUsers = users;
      this.loadMessages();
    });
  }

  loadMessages() {
    if (this.adminUsers.length > 0) {
      this.messageService.getMessages(this.user.id, this.adminUsers[0]?.id,0,20).subscribe(
        (messages: Message[]) => {
          this.messages = messages;
        },
        (error) => {
          console.error('Error occurred:', error);
        }
      );
    } else {
      console.warn('No admin users found, cannot load messages.');
    }
  }
  sendMessage() {
    if (this.content) {

      const message: Message = {
        id: 0,
        senderId: this.user.id,
        recipientId: this.adminUsers[0].id,
        content: this.content,
        fileUrl: '',
        fileType: '',
        sentAt: new Date().toISOString()
      }
      this.messageService.sendMessage(message).subscribe(
        (response) => {
          console.log(response);
          this.chatService.sendMessage(message);
          this.content = '';
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  formatMessageTime(sentAt: string): string {
    const date = new Date(sentAt);
    return `${date.getHours()}:${date.getMinutes()}`;
  }

}
