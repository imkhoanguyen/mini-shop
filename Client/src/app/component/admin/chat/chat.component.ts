import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  inject,
  OnInit,
  Type,
  ViewChild,
} from '@angular/core';
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
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  selectedUser: any;
  messages: Message[] = [];
  customers: User[] = [];
  user!: User;
  content: string = '';
  loadingOldMessages = false;
  lastMessage: string = '';
  skip: number = 0;
  messageId: number = 0;
  selectedFiles: { src: string; file: File; type: string }[] = [];

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
  loadUsersWithCustomerRole() {
    this.userService.getUsersWithCustomerRole().subscribe((users) => {
      this.customers = users;
      this.customers.forEach((customer) => {
        this.messageService.getLastMessage(this.user.id, customer.id)
        .subscribe((message) => {
          customer.lastMessage = message?.content || '';

        });
      })
    });
  }
  onScroll() {
    const element = this.messagesContainer.nativeElement;
    if (element.scrollTop === 0 && !this.loadingOldMessages) {
      this.loadOldMessages();
    }
  }

  loadOldMessages() {
    if (!this.selectedUser) return;
    this.loadingOldMessages = true;
    const currentSkip = this.messages.length;

    this.messageService
      .getMessages(this.user.id, this.selectedUser.id, currentSkip, 10)
      .subscribe(
        (messages: Message[]) => {
          if (messages.length > 0) {
            this.messageId = messages[0].id;
            this.messages = [...messages.reverse(), ...this.messages];
          }
          this.skip += messages.length;
          this.loadingOldMessages = false;
        },
        (error) => {
          console.error('Error occurred:', error);
          this.loadingOldMessages = false;
        }
      );
  }

  selectUser(customer: any) {
    this.selectedUser = customer;
    this.messages = [];
    this.loadOldMessages();
    this.scrollToBottom();
  }
  scrollToBottom() {
    const element = this.messagesContainer.nativeElement;
    setTimeout(() => {
      element.scrollTop = element.scrollHeight;
    }, 100);
  }
  onFileSelected(event: any) {
    const files: FileList = event.target.files;
    const newFiles: { src: string; file: File; type: string }[] = [];

    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const fileType = file.type;

      const reader = new FileReader();
      reader.onload = (e: any) => {
        if (fileType.startsWith('image/')) {
          newFiles.push({
            src: e.target.result,
            file,
            type: 'image',
          });
        } else {
          newFiles.push({
            src: e.target.result,
            file,
            type: 'video',
          });
        }
        if (i === files.length - 1) {
          this.selectedFiles = [...this.selectedFiles, ...newFiles];
        }
      };
      if (fileType.startsWith('image/') || fileType.startsWith('video/')) {
        reader.readAsDataURL(file);
      }
    }
  }

  onPaste(event: ClipboardEvent) {
    const items = Array.from(event.clipboardData?.items || []);
    items.forEach((item) => {
      if (item.type.indexOf('image') !== -1) {
        const file = item.getAsFile();
        if (file) {
          const reader = new FileReader();
          reader.onload = (e: any) => {
            this.selectedFiles.push({
              src: e.target.result,
              file,
              type: 'image',
            });
          };
          reader.readAsDataURL(file);
        }
      }
    });
  }
  removeImage(index: number) {
    this.selectedFiles.splice(index, 1);
  }
  isImage(fileType: string | undefined): boolean {
    return fileType ? fileType.startsWith('image/') : false;
  }

  isVideo(fileType: string | undefined): boolean {
    return fileType ? fileType.startsWith('video/') : false;
  }
  isRecipient(recipientIds: string[], userId: string): boolean {
    return recipientIds.includes(userId);
  }

  replyMessage() {
    if (!this.content && this.selectedFiles.length === 0) {
      return;
    }
    console.log("id", this.messageId);
    console.log("user", this.user.id);
    this.messageService.getMessageById(this.messageId).subscribe((replyMessage : Message)=> {
      if(!replyMessage.isReplied && replyMessage.repliedById == null){
        this.messageService.replyMessage(this.messageId, this.user.id).subscribe(()=>{
          if (this.selectedFiles.length > 0) {
            const formData = new FormData();
            this.selectedFiles.forEach((file) => {
              formData.append('files', file.file);
            });

            this.messageService.uploadFiles(formData).subscribe((response: any) => {
              console.log(response);
              for (let i = 0; i < response.files.length; i++) {
                const message: Message = {
                  id: 0,
                  senderId: this.user.id,
                  recipientIds: [this.selectedUser.id],
                  content: this.content || '',
                  fileUrl: response.files[i].fileUrl,
                  fileType: response.files[i].fileType,
                  repliedById: this.user.id,
                  isReplied: true,
                  sentAt: new Date().toISOString(),
                };
                console.log("message", message);
                this.sendMessageToServer(message);
              }
              this.resetMessageInput();
            });
          } else {
            const message: Message = {
              id: 0,
              senderId: this.user.id,
              recipientIds: [this.selectedUser.id],
              content: this.content || '',
              fileUrl: null,
              fileType: undefined,
              repliedById: this.user.id,
              isReplied: true,
              sentAt: new Date().toISOString(),
            };
            console.log("message1", message);
            this.sendMessageToServer(message);
            this.resetMessageInput();
          }
        });
      }
      if (replyMessage.isReplied && replyMessage.repliedById === this.user.id) {
        if (this.selectedFiles.length > 0) {
          const formData = new FormData();
          this.selectedFiles.forEach((file) => {
            formData.append('files', file.file);
          });

          this.messageService.uploadFiles(formData).subscribe((response: any) => {
            console.log(response);
            for (let i = 0; i < response.files.length; i++) {
              const message: Message = {
                id: 0,
                senderId: this.user.id,
                recipientIds: [this.selectedUser.id],
                content: this.content || '',
                fileUrl: response.files[i].fileUrl,
                fileType: response.files[i].fileType,
                repliedById: this.user.id,
                isReplied: true,
                sentAt: new Date().toISOString(),
              };
              console.log("message", message);
              this.sendMessageToServer(message);
            }
            this.resetMessageInput();
          });
        } else {
          const message: Message = {
            id: 0,
            senderId: this.user.id,
            recipientIds: [this.selectedUser.id],
            content: this.content || '',
            fileUrl: null,
            fileType: undefined,
            repliedById: this.user.id,
            isReplied: true,
            sentAt: new Date().toISOString(),
          };
          console.log("message1", message);
          this.sendMessageToServer(message);
          this.resetMessageInput();
        }
      } else {
        console.log("You cannot reply to this message.");
        alert("tin nhắn đã được trả lời bởi nhân viên khác");
      }
    }, (error) => {
      console.log(error);
    });
  }



  sendMessageToServer(message: Message) {
    this.messageService.sendMessage(message).subscribe(
      (response) => {
        console.log(response);
        this.chatService.sendMessage(message);
        this.lastMessage = message.content || '';
        const selectedCustomer = this.customers.find(
          (customer) => customer.id === this.selectedUser.id
        );
        if (selectedCustomer) {
          selectedCustomer.lastMessage = this.lastMessage;
        }
        this.scrollToBottom();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  resetMessageInput() {
    this.content = '';
    this.selectedFiles = [];
  }
}
