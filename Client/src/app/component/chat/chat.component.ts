import { CommonModule } from '@angular/common';
import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/user.module';
import { AccountService } from '../../_services/account.service';
import { UserService } from '../../_services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from '../../_services/message.service';
import { Message } from '../../_models/message.module';
import { ChatService } from '../../_services/chat.service';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  showChatWindow = false;
  adminUsers: User[] = [];
  messages: Message[] = [];
  user!: User;
  content: string = '';
  loadingOldMessages = false;
  skip: number = 0;
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
    this.loadUsersWithAdminRole();
    this.chatService.onMessageReceived((content) => {
      this.messages.push(content);

    });
    setTimeout(() => {
      this.scrollToBottom();
    }, 100);
  }

  toggleChatWindow() {
    this.showChatWindow = !this.showChatWindow;
    if (this.showChatWindow) {
      setTimeout(() => {
        this.scrollToBottom();
      }, 10);
    }
  }
  loadUsersWithAdminRole() {
    this.userService.getUsersWithAdminRole().subscribe((users) => {
      this.adminUsers = users;
      this.loadOldMessages();
    });
  }
  onScroll() {
    const element = this.messagesContainer.nativeElement;
    if (element.scrollTop === 0 && !this.loadingOldMessages) {
      this.loadOldMessages();
    }
  }
  loadOldMessages() {
    this.loadingOldMessages = true;
    const currentSkip = this.messages.length;

    this.messageService
      .getMessages(this.user.id, this.adminUsers[0].id, currentSkip, 10)
      .subscribe(
        (messages: Message[]) => {
          if (messages.length > 0) {
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
  scrollToBottom() {
    const element = this.messagesContainer.nativeElement;
    setTimeout(() => {
      element.scrollTop = element.scrollHeight;
    }, 100);
  }
  sendMessage() {

    if (!this.content && this.selectedFiles.length === 0) {
      return;
    }

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
            recipientId: this.adminUsers[0].id,
            content: this.content || '',
            fileUrl: response.files[i].fileUrl,
            fileType: response.files[i].fileType,
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
        recipientId: this.adminUsers[0].id,
        content: this.content,
        fileUrl: null,
        fileType: undefined,
        sentAt: new Date().toISOString(),
      };
      console.log("message1", message);
      this.sendMessageToServer(message);
      this.resetMessageInput();
    }
  }

  sendMessageToServer(message: Message) {
    console.log("message", message);
    this.messageService.sendMessage(message).subscribe(
      (response) => {
        console.log(response);
        this.chatService.sendMessage(message);

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
