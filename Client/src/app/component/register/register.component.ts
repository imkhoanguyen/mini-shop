import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidatorFn  } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Register } from '../../_models/register.module';
import { AccountService } from '../../_services/account.service';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ButtonModule,ReactiveFormsModule, CommonModule, RouterModule, ToastModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  providers: [MessageService]
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  registrationError: string | null = null;
  accountService = inject(AccountService);
  constructor(private router: Router, private messageService: MessageService){}

  ngOnInit(){
    this.registerForm = new FormGroup({
      fullname: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    },{ validators: this.passwordMatchValidator });
  }
  passwordMatchValidator: ValidatorFn = (control: AbstractControl): { [key: string]: boolean } | null => {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return password && confirmPassword && password !== confirmPassword ? { passwordMismatch: true } : null;
  }
  loginForm(){
    this.router.navigateByUrl('/login');

  }
  private showMessage(severity: string, detail: string): void {
    const summary: string = '';
    const life: number = 3000;
    if (severity === 'error') {
      this.messageService.add({ severity, summary: 'Thất Bại', detail, life });
    } else if (severity === 'success') {
      this.messageService.add({severity,summary: 'Thành Công',detail,life,});
    } else {
      this.messageService.add({ severity, summary: 'Cảnh báo', detail, life });
    }
  }
  onSubmit() {
    const data: Register = {
      fullname: this.registerForm.get('fullname')?.value,
      email: this.registerForm.get('email')?.value,
      username: this.registerForm.get('username')?.value,
      password: this.registerForm.get('password')?.value
    };
    if(this.registerForm.invalid){
      this.showMessage('error', 'Vui lòng nhập đầy đủ thông tin');
      return;
    }
    this.accountService.register(data).subscribe(
      (res: any) => {
        console.log("Register success", res);
        this.showMessage('success', 'Đăng ký thành công. Đang chuyển sang đăng nhập');
        setTimeout(() => {
          this.router.navigateByUrl('/login');
        }, 200);
      },
      (error) => {
        this.showMessage('error', error);
      }
    );
  }
}
