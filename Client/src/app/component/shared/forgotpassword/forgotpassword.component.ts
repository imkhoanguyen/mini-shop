import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../_services/account.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-forgotpassword',
  standalone: true,
  imports: [FormsModule, ToastModule],
  templateUrl: './forgotpassword.component.html',
  styleUrl: './forgotpassword.component.css',
  providers: [MessageService],
})
export class ForgotpasswordComponent {
  private accountService = inject(AccountService);
  constructor(private messageService: MessageService) {}

  email: string = '';
  errorMessage: string = '';
  onSubmit() {
    this.errorMessage = '';

    if (!this.email) {
      this.errorMessage = 'Email không được để trống';
      return;
    }

    const gmailRegex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
    if (!gmailRegex.test(this.email)) {
      this.errorMessage = 'Vui lòng nhập email hợp lệ có đuôi @gmail.com';
      return;
    }

    this.accountService.forgotPassword(this.email).subscribe({
      next: (response) => {
        console.log((response as any).message);
        this.showSuccess((response as any).message);
      },
      error: (er) => {
        console.log(er);
        this.showError(er.error);
      },
    });
  }

  showError(detail: string, summary?: string) {
    this.messageService.add({
      severity: 'error',
      summary: summary || 'Error',
      detail: detail,
      life: 3000,
    });
  }

  showSuccess(detail: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: detail,
      life: 3000,
    });
  }
}
