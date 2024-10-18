import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidatorFn  } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Register } from '../../_models/register.module';
import { AccountService } from '../../_services/account.service';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ButtonModule,ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  providers: [MessageService]
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
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
  onSubmit(){
    const data : Register = {
      fullname: this.registerForm.get('fullname')?.value,
      email: this.registerForm.get('email')?.value,
      username: this.registerForm.get('username')?.value,
      password: this.registerForm.get('password')?.value
    }
    console.log("register", data)
    this.accountService.register(data).subscribe(
      (res: any) => {
        console.log("Register success", res);
        this.router.navigateByUrl('/login');
      },
      (error) => {
        console.log("Register failed", error);
      }
    )
  }
}
