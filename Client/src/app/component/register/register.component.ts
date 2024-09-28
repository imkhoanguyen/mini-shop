import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Register } from '../../_models/register.module';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ButtonModule,ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm!: FormGroup;
  constructor(private router: Router,
              private accountService: AccountService)
  {
    this.registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
    });
  }
  loginForm(){
    this.router.navigateByUrl('/login');

  }
  onSubmit(){
    const data : Register = {
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
