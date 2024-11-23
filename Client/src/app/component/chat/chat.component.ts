import { CommonModule } from '@angular/common';
import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/user.module';
import { AccountService } from '../../_services/account.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from '../../_services/message.service';
import { ChatService } from '../../_services/chat.service';
import { MessageDto } from '../../_models/message.module';
import { ToastrService } from '../../_services/toastr.service';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  showChatWindow = false;
  messages: MessageDto[] = [];
  recipientUser!: User;
  user!: User;
  content: string = '';
  loadingOldMessages = false;
  selectedFiles: { src: string; file: File; type: string }[] = [];
  recipientId: string = '';
  @ViewChild('messagesContainer') messagesContainer!: ElementRef;

  private chatService = inject(ChatService);
  private accountService = inject(AccountService);
  private messageService = inject(MessageService);
  constructor() {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      this.user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(this.user);
    }
  }
  ngOnInit() {
    this.setupMessageCustomerReceived();
    setTimeout(() => {
      this.scrollToBottom();
    }, 100);
      this.loadMessages();
  }

  toggleChatWindow() {
    this.showChatWindow = !this.showChatWindow;
    if (this.showChatWindow) {
      setTimeout(() => {
        this.scrollToBottom();
      }, 10);
    }

  }
  onScroll() {

    const element = this.messagesContainer.nativeElement;
    if (element.scrollTop === 0 && !this.loadingOldMessages) {
      this.loadMessages();
    }

  }
  private setupMessageCustomerReceived() {
    console.log("setupMessageReceived called");
    this.chatService.messageReceived$.subscribe({
      next: (message: MessageDto | null) => {
        if (message) {
          console.log('Message received:', message);
          this.messages.push(message);
          this.scrollToBottom();
        }
      },
      error: (err) => console.error('Error receiving message:', err),
    });
  }


  loadMessages() {
    this.messageService
      .getMessages(this.user.id)
      .subscribe(
        (messages: MessageDto[]) => {
          this.messages = messages;
        },
        (error) => {
          console.error('Error occurred:', error);
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
    if (!this.content) {
      return;
    }
    const formData = new FormData();
    formData.append('senderId', this.user.id);
    formData.append('recipientIds', '');
    formData.append('content', this.content);
    this.selectedFiles.forEach((file) => {
      formData.append('files', file.file);
    });
    this.messageService.addMessage(formData).subscribe({
      next: (response: MessageDto) => {
        console.log("response", response);
        this.chatService.sendMessage(response);
        this.loadMessages();
        this.scrollToBottom();
        this.resetMessageInput();
      },
      error: (error) => {
        console.error('Error occurred:', error);
      },
    });
  }

  resetMessageInput() {
    this.content = '';
    this.selectedFiles = [];
  }



}
