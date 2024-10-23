import { CommonModule } from '@angular/common';
import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Message } from '../../../_models/message.module';
import { User } from '../../../_models/user.module';
import { AccountService } from '../../../_services/account.service';
import { UserService } from '../../../_services/user.service';
import { MessageService } from '../../../_services/message.service';
import { ChatService } from '../../../_services/chat.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit {
  selectedUser: any;
  messages: Message[] = [];
  customers: User[] = [];
  user!: User;
  content: string = '';
  loadingOldMessages = false;
  lastMessage: string = '';

  @ViewChild('messagesContainer') messagesContainer!: ElementRef;
  private chatService = inject(ChatService);
  private accountService = inject(AccountService);
  private userService = inject(UserService);
  private messageService = inject(MessageService);
  constructor() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      this.user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(this.user);
    }
  }

  ngOnInit() {
    this.loadUsersWithCustomerRole();
    this.chatService.onMessageReceived((content) => {
      this.messages.push(content);
      this.scrollToBottom();
    });
  }
  loadUsersWithCustomerRole(){
      this.userService.getUsersWithCustomerRole().subscribe((users) => {
        this.customers = users;
        this.customers.forEach((customer) => {
          this.messageService.getLastMessage(this.user.id, customer.id).subscribe((message) => {
            customer.lastMessage = message.content || '';
          });
        });
      });
    }
  onScroll(){
    const element = this.messagesContainer.nativeElement;
    if(element.scrollTop === 0 && !this.loadingOldMessages){
      this.loadOldMessages();
    }
  }

  loadOldMessages(){
    if(!this.selectedUser) return;
    this.loadingOldMessages = true;
    const skip = this.messages.length;

    this.messageService.getMessages(this.user.id, this.selectedUser.id, skip, 20).subscribe(
      (messages: Message[]) => {
        if (messages.length > 0) {

          this.messages = [...messages, ...this.messages];


        }
        this.loadingOldMessages = false;
      },
      (error) => {
        console.error('Error occurred:', error);
        this.loadingOldMessages = false;
      }
    );
  }

  selectUser(customer: any){
    this.selectedUser  = customer;
    this.messages = [];
    this.loadOldMessages();
    this.scrollToBottom();
  }
  scrollToBottom(){
    const element = this.messagesContainer.nativeElement;
    setTimeout(() => {
      element.scrollTop = element.scrollHeight;
    }, 100);
  }

  sendMessage(){
    if (this.content) {
      const message: Message = {
        id: 0,
        senderId: this.user.id,
        recipientId: this.selectedUser.id,
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
          this.lastMessage = message.content || '';
          const selectedCustomer = this.customers.find(customer => customer.id === this.selectedUser.id);
          if (selectedCustomer) {
            selectedCustomer.lastMessage = this.lastMessage; // Cập nhật lastMessage cho customer hiện tại
          }
          this.scrollToBottom();

        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
