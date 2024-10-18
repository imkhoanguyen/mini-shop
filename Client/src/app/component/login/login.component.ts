declare var google: any;
import { Component, inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Login } from '../../_models/login.module';
import { AccountService } from '../../_services/account.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ButtonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoggedIn = false;
  user: any;
  private accountService = inject(AccountService);
  private router = inject(Router);
  ngOnInit(): void {

    google.accounts.id.initialize({
      client_id: '306838179156-gha4u6q5u2pfvti0e1b25fm9bf0067js.apps.googleusercontent.com',
      callback: (response: any) => this.handleLogin(response)
    });
    google.accounts.id.renderButton(document.getElementById("google-btn"),{
      theme: 'filled_blue',
      size: 'large',
      share: 'rectangle',
      width: '200'
    });
    this.loginForm = new FormGroup({
      userNameOrEmail: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required),
    });
  }

  registerForm() {
    this.router.navigateByUrl('/register');
  }
  onSubmit() {
    const data: Login = {
      userNameOrEmail: this.loginForm.value.userNameOrEmail,
      password: this.loginForm.value.password,
    };
    console.log('login', data);
    this.accountService.login(data).subscribe(
      (res) => {
        this.router.navigateByUrl('/');
      },
      (error) => {
        console.log('Login failed', error);
      }
    );
  }
  private decodeToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
  handleLogin(response: any) {
    if(response){
      const payLoad = this.decodeToken(response.credential);
      localStorage.setItem('token', JSON.stringify(payLoad));
      const userInfo = {
        fullname: payLoad.name,
        email: payLoad.email,
        picture: payLoad.picture,
      };
      localStorage.setItem('userInfo', JSON.stringify(userInfo));

      this.router.navigateByUrl('/');
    }
  }

}
