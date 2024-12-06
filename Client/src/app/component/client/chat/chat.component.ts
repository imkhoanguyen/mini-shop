import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { User } from '../../../_models/user.module';
import { AccountService } from '../../../_services/account.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from '../../../_services/message.service';
import { ChatService } from '../../../_services/chat.service';
import { MessageDto } from '../../../_models/message.module';
import { ToastrService } from '../../../_services/toastr.service';
import { PaginatedResult, Pagination } from '../../../_models/pagination';
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
  pagination: Pagination = {
    currentPage: 1,
    itemPerPage: 10,
    totalItems: 0,
    totalPages: 1,
  };

  params = {
    pageNumber: 1,
    pageSize: 10,
    search: '',
  };
  addPageSize: number = 5;
  loading = false;

  selectedFiles: { src: string; file: File; type: string }[] = [];
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
    this.chatService.joinGroup('', this.user.id);
  }
  onScroll() {
    const element = this.messagesContainer.nativeElement;
    const previousHeight = element.scrollHeight;

    if (element.scrollTop === 0 && !this.loading) {
      if (this.pagination.currentPage < this.pagination.totalPages) {
        this.params.pageNumber++;
        this.loadMessages();
        setTimeout(() => {
          const currentHeight = element.scrollHeight;
          element.scrollTop = currentHeight - previousHeight;
        }, 100);
      }
    }
  }
  private setupMessageCustomerReceived() {
    console.log('setupMessageCustomerReceived called');
    this.chatService.messageReceived$.subscribe({
      next: (message: MessageDto | null) => {
        if (message && message.senderId !== this.user.id) {
          // Kiểm tra senderId
          console.log('Message received from admin:', message);
          this.messages.push(message);
          this.scrollToBottom();
        }
      },
      error: (err) => console.error('Error receiving message:', err),
    });
  }

  loadMessages() {
    if (this.loading) return;
    this.loading = true;

    this.messageService
      .getMessageThread(this.params, this.user.id)
      .subscribe((result) => {
        if (result.items) {
          this.messages = [...result.items.reverse(), ...this.messages];
        }
        console.log('messages', this.messages);
        this.pagination = result.pagination || {
          currentPage: 1,
          itemPerPage: 10,
          totalItems: 0,
          totalPages: 1,
        };
        this.loading = false;
      });
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
    if (!this.content.trim() && this.selectedFiles.length === 0) {
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
        this.messages.push(response);
        this.chatService.sendMessage(response);
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
