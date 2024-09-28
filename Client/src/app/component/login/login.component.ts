import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Login } from '../../_models/login.module';
import { AccountService } from '../../_services/account.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ButtonModule,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;
  constructor(private router: Router,
              private accountService: AccountService
  ) {
    this.loginForm = new FormGroup({
    userOrEmail: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    });
  }
  registerForm(){
    this.router.navigateByUrl('/register');

  }
  onSubmit(){
    const data: Login = {
      usernameOrEmail: this.loginForm.value.userOrEmail,
      password: this.loginForm.value.password
    }
    console.log("login", data);
    this.accountService.login(data).subscribe(
      (res: any) => {
        console.log("Login success", res);
        this.router.navigateByUrl('/');
      },
      (error) => {
        console.log("Login failed", error);
      }
    )
  }
}
