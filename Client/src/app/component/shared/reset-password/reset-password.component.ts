import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { AccountService } from '../../../_services/account.service';
import { ResetPassword } from '../../../_models/resetPassword';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [FormsModule, ToastModule, CommonModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
  providers: [MessageService],
})
export class ResetPasswordComponent implements OnInit {
  private accountService = inject(AccountService);
  constructor(
    private messageService: MessageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}
  private resetPassword: ResetPassword = {
    email: '',
    password: '',
    token: '',
  };
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.resetPassword.email = params['email'];
      this.resetPassword.token = params['token'];
    });
  }
  password: string = '';
  confirmPassword: string = '';
  passwordErrorMessage: string = '';

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPasswordVisibility() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onSubmit() {
    this.passwordErrorMessage = '';

    if (!this.password) {
      this.passwordErrorMessage = 'Mật khẩu không được để trống';
    }

    if (this.password !== this.confirmPassword) {
      this.passwordErrorMessage = 'Mật khẩu xác nhận không khớp!';
      return;
    }

    this.resetPassword.password = this.password;
    this.accountService.resetPassword(this.resetPassword).subscribe({
      next: (response) => {
        this.showSuccess((response as any).message);
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (er) => {
        console.log(er);
        if (er.error === 'Invalid token.') {
          this.showError(er.error);
        } else {
          this.passwordErrorMessage = er.error;
        }
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
