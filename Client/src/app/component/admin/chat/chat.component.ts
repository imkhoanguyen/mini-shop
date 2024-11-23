import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  inject,
  OnDestroy,
  OnInit,
  Renderer2,
  Type,
  ViewChild,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessageDto } from '../../../_models/message.module';
import { User } from '../../../_models/user.module';
import { AccountService } from '../../../_services/account.service';
import { MessageService } from '../../../_services/message.service';
import { ChatService } from '../../../_services/chat.service';
import { ToastrService } from '../../../_services/toastr.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit, OnDestroy {
  selectedUser: any;
  message: string = '';
  isTyping: boolean = false;
  messages: MessageDto[] = [];
  typingAdminId: string | null = null;
  recipientUser!: User;
  customers: User[] = [];
  user!: User;
  content: string = '';
  loadingOldMessages = false;
  selectedFiles: { src: string; file: File; type: string }[] = [];
  recipientId: string = '';
  searchQuery = '';

  @ViewChild('messagesContainer') messagesContainer!: ElementRef;

  private chatService = inject(ChatService);
  private accountService = inject(AccountService);
  private messageService = inject(MessageService);
  private toastrService = inject(ToastrService);
  constructor(private renderer: Renderer2) {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      this.user = JSON.parse(userJson) as User;
      this.accountService.setCurrentUser(this.user);
    }
  }
  ngOnInit() {
    this.getCustomers();
    this.setupMessageReceived();
    this.setupTypingStatus();
    this.renderer.listen('document', 'visibilitychange', this.onVisibilityChange.bind(this));
  }
  ngOnDestroy(): void {
    document.removeEventListener('visibilitychange', this.onVisibilityChange);
  }
  filterCustomers() {
    this.customers = this.customers.filter((customer) =>
      customer.fullName.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
  onVisibilityChange(): void {
    if (document.visibilityState === 'visible') {
      this.notifyTyping();
    }
  }

  private setupMessageReceived() {
    console.log('setupMessageReceived');
    this.chatService.messageReceived$.subscribe({
      next: (message: MessageDto | null) => {
        if (message) {
          console.log('receivedfromUser', message);

          this.messages.push(message);

          this.typingAdminId = null;
        }
      },
      error: (err) => console.error('Error receiving message:', err),
    });
  }



  private setupTypingStatus() {
    document.addEventListener('visibilitychange', () => {
      if (document.visibilityState === 'visible' && this.content) {
        this.notifyTyping();
      }
    });

    this.chatService.typingStatus$.subscribe({
      next: (status) => {
        if (status && this.content !== null) {
          this.typingAdminId = status.isTyping ? status.adminId : null;
        } else {
          this.typingAdminId = null;
        }
      },
      error: (err) => console.error('Error updating typing status:', err),
    });
  }
  getCustomers() {
    this.messageService.getCustomers().subscribe({
      next: (response : string[]) => {

        for (let customer of response) {
          this.accountService.getUserId(customer).subscribe({
            next: (user : User) => {
              this.customers.push(user);
            },
            error: (error) => {
              console.error('Error occurred while fetching user info:', error);
            },
          });
        }
      },
      error: (error) => {
        console.error('Error occurred:', error);
      },
    });
  }
  selectUser(customer: any) {
    this.selectedUser = customer;
    this.messages = [];
    this.loadMessages(this.selectedUser.id);
    this.scrollToBottom();

    this.chatService.joinGroup(this.selectedUser.id, this.user.id);
    if (this.typingAdminId) {
      this.typingAdminId = null;
    }
  }
  onScroll() {
    const element = this.messagesContainer.nativeElement;
    if (element.scrollTop === 0 && !this.loadingOldMessages) {
     // this.loadMessages();
    }
  }
  getLastMessage(callback?: () => void) {
    this.messageService.getLastMessage(this.user.id).subscribe(
      (message: MessageDto) => {
        if (message) {
          this.recipientId = message.repliedById || '';
          this.accountService.getUserId(this.recipientId).subscribe({
            next: (response : User) => {
              this.recipientUser = response;
            },
            error: (error) =>{
              console.log("Lỗi"+ error);
            }
          })
        }
        if (callback) {
          callback();
        }
      },
      (error) => {
        console.error('Error occurred:', error);
        if (callback) {
          callback();
        }
      }
    );
  }

  loadMessages(customerId: string) {
    this.messageService
      .getMessages(customerId)
      .subscribe(
        (messages: MessageDto[]) => {
          this.messages = messages;
          console.log('messages', this.messages);
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
  notifyTyping(): void {
    this.isTyping = true;
    this.chatService.notifyTyping(this.selectedUser!.id, this.user.id);
  }

  stopTyping(): void {
    this.isTyping = false;
    this.chatService.stopTyping(this.selectedUser!.id, this.user.id);
  }

  sendMessage() {
    if (!this.content) {
      return;
    }
    const formData = new FormData();
    formData.append('senderId', this.user.id);
    formData.append('recipientIds', this.selectedUser!.id);
    formData.append('content', this.content);
    this.selectedFiles.forEach((file) => {
      formData.append('files', file.file);
    });
    this.messageService.addMessage(formData).subscribe({
      next: (response: MessageDto) => {
        console.log("response", response);
        this.chatService.sendMessage(response);
        this.loadMessages(this.selectedUser!.id);
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
