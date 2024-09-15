import { AfterViewInit, Component } from "@angular/core";
import { SharedModule } from "../../shared/shared.module";
import { FormControl, FormGroup, Validators } from "@angular/forms";


@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent implements AfterViewInit {
  value: string = '';
  blockSpace: RegExp = /[^s]/;
  authForm: FormGroup;

  constructor(){
  this.authForm = new FormGroup({
  email: new FormControl('', [Validators.required, Validators.email]),
  password: new FormControl('', Validators.required),
  confirmPassword: new FormControl('', Validators.required)
  });
  }

  ngAfterViewInit() {
  const container = document.getElementById('container');
  const registerBtn = document.getElementById('register');
  const loginBtn = document.getElementById('login');

  if (container && registerBtn && loginBtn) {
  loginBtn.addEventListener('click', () => {
  container.classList.add("active");
  });

  registerBtn.addEventListener('click', () => {
  container.classList.remove("active");
  });
  }
  }
}

