import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  HostListener,
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
import { PaginatedResult, Pagination } from '../../../_models/pagination';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit, OnDestroy {
  private messageSubscription!: Subscription;
  selectedUser: any;
  message: string = '';
  isTyping: boolean = false;
  messages: MessageDto[] = [];
  typingAdminId: string | null = null;
  recipientUser!: User;
  searchText: string = '';
  customers: User[] = [];
  filteredCustomers: User[] = [];
  user!: User;
  content: string = '';
  lastMessage: string = '';
  pagination: Pagination = { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };
  selectedFiles: { src: string; file: File; type: string }[] = [];
  recipientId: string = '';
  params = {
    pageNumber: 1,
    pageSize: 10,
    search: ''
  };

  addPageSize: number = 5;
  loading = false;
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

  onVisibilityChange(): void {
    if (document.visibilityState === 'visible') {
      this.notifyTyping();
    }
  }

  private setupMessageReceived() {
    if (this.messageSubscription) {
      this.messageSubscription.unsubscribe();
    }

    this.chatService.typingStatus$.subscribe({
      next: (status) => {
        if (status && this.content !== null && status.customerId === this.selectedUser.id) {
          this.typingAdminId = status.isTyping ? status.adminId : null;
        } else {
          this.typingAdminId = null;
        }
      },
      error: (err) => console.error('Error updating typing status:', err),
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
              this.filteredCustomers.push(user);
              this.getLastMessage(user.id);
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

  onSearch(): void {
    if(this.searchText) {
      console.log("searchText", this.searchText);
      this.filteredCustomers = this.customers.filter((customer) => {
        return customer.fullName.toLowerCase().includes(this.searchText.toLowerCase());
      });
    }
    else {
      this.filteredCustomers = this.customers;
    }
    console.log("a",this.filteredCustomers);
  }

  selectUser(customer: any) {
    this.selectedUser = customer;
    this.messages = [];
    this.params ={
      pageNumber: 1,
      pageSize: 10,
      search: ''
    }
    console.log("selectedUser", this.selectedUser);
    this.loadMessages(this.selectedUser.id);
    this.scrollToBottom();

    this.chatService.joinGroup(this.selectedUser.id, this.user.id);
    if (this.typingAdminId) {
      this.typingAdminId = null;
    }
  }
  onScroll() {
    const element = this.messagesContainer.nativeElement;
    const previousHeight = element.scrollHeight;

    if (element.scrollTop === 0 && !this.loading) {
      if (this.pagination.currentPage < this.pagination.totalPages) {
        this.params.pageNumber++;
        this.loadMessages(this.selectedUser.id);
        setTimeout(() => {
          const currentHeight = element.scrollHeight;
          element.scrollTop = currentHeight - previousHeight;
        }, 100);
      }
    }
  }

  getLastMessage(customerId: string) {
    this.messageService.getLastMessage(customerId).subscribe(
      (message: MessageDto) => {
        const user = this.customers.find(c => c.id === customerId);
        if (user) {
          user.lastMessage = message?.content ?? '';
        }
      },
      (error) => {
        console.error('Error occurred:', error);
      }
    );
  }

  loadMessages(customerId: string) {
    if (this.loading) return;
    this.loading = true;

    this.messageService.getMessageThread(this.params, customerId).subscribe(result => {
      if (result.items) {
        this.messages = [...result.items.reverse(), ...this.messages];
      }
      console.log("messages", this.messages);
      this.pagination = result.pagination || { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };
      this.loading = false;
    });
  }


  onSearchText(searchTerm: string) {
    this.params.search = searchTerm;
    this.params.pageNumber = 1;
    this.messages = [];
    this.loadMessages(this.selectedUser.id);
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
    if (this.selectedUser && this.user) {
      this.isTyping = true;
      this.chatService.notifyTyping(this.selectedUser.id, this.user.id);
    }
  }

  stopTyping(): void {
    if (this.selectedUser && this.user) {
      this.isTyping = false;
      this.chatService.stopTyping(this.selectedUser.id, this.user.id);
    }
  }

  sendMessage() {
    if (!this.content.trim() && this.selectedFiles.length === 0) {
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
        this.messages.push(response);
        this.chatService.sendMessage(response);
        this.selectedUser!.lastMessage = response.content;
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
