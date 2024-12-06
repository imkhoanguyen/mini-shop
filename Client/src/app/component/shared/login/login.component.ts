declare var google: any;
declare var FB: any;
import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { AccountService } from '../../../_services/account.service';
import { User } from '../../../_models/user.module';
import { Login } from '../../../_models/login.module';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ButtonModule, ReactiveFormsModule, RouterModule, ToastModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [MessageService, OAuthService],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoggedIn = false;
  user: any;
  sdkReady: any;
  private accountService = inject(AccountService);
  private router = inject(Router);
  private oauthService = inject(OAuthService);
  private messageService = inject(MessageService);
  constructor() {
    this.configureOAuth();
  }
  private configureOAuth() {
    const authConfig: AuthConfig = {
      issuer: 'https://accounts.google.com',
      redirectUri: window.location.origin,
      clientId:
        '306838179156-gha4u6q5u2pfvti0e1b25fm9bf0067js.apps.googleusercontent.com',
      responseType: 'code',
      scope: 'openid profile email',
      showDebugInformation: true,
      strictDiscoveryDocumentValidation: false,
    };

    this.oauthService.configure(authConfig);
    this.oauthService.setupAutomaticSilentRefresh();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    this.oauthService.events.subscribe((e) => {
      if (e.type === 'token_received') {
        this.handleLogin(e);
      }
    });
  }
  ngOnInit(): void {
    this.initializeForm();
    this.initializeGoogleLogin();
    this.initializeFacebookLogin();
  }
  initializeForm() {
    const fb = new FormBuilder();
    this.loginForm = fb.group({
      userNameOrEmail: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  private initializeGoogleLogin() {
    google.accounts.id.initialize({
      client_id:
        '306838179156-gha4u6q5u2pfvti0e1b25fm9bf0067js.apps.googleusercontent.com',
      callback: (response: any) => this.handleGoogleLogin(response),
    });
    google.accounts.id.renderButton(document.getElementById('google-btn'), {
      type: 'icon',
      theme: 'filled_blue',
      size: 'large',
    });
  }
  private isFbSdkLoaded(): boolean {
    return typeof FB !== 'undefined';
  }

  private initializeFacebookLogin() {
    if (this.isFbSdkLoaded()) {
      this.setupFacebookSDK();
    } else {
      (window as any)['fbAsyncInit'] = () => {
        this.setupFacebookSDK();
      };

      // Tải SDK Facebook
      const scriptTag = document.createElement('script');
      scriptTag.id = 'facebook-jssdk';
      scriptTag.src = 'https://connect.facebook.net/en_US/sdk.js';
      document.getElementsByTagName('head')[0].appendChild(scriptTag);
    }
  }

  private setupFacebookSDK() {
    FB.init({
      appId: '874958284774933',
      xfbml: true,
      version: 'v21.0',
    });
    this.sdkReady = true;
  }
  private showMessage(severity: string, detail: string): void {
    const summary: string = '';
    const life: number = 3000;
    if (severity === 'error') {
      this.messageService.add({ severity, summary: 'Thất Bại', detail, life });
    } else if (severity === 'success') {
      this.messageService.add({
        severity,
        summary: 'Thành Công',
        detail,
        life,
      });
    } else {
      this.messageService.add({ severity, summary: 'Cảnh báo', detail, life });
    }
  }
  registerForm() {
    this.router.navigateByUrl('/register');
  }
  onSubmit() {
    const data: Login = {
      userNameOrEmail: this.loginForm.value.userNameOrEmail,
      password: this.loginForm.value.password,
    };
    if (!data.userNameOrEmail || !data.password) {
      this.showMessage('error', 'Vui lòng nhập đầy đủ thông tin');
    }
    this.accountService.login(data).subscribe(
      (res) => {
        window.location.href = '/';
      },
      (error) => {
        this.showMessage('error', error);
      }
    );
  }

  handleLogin(response: any) {
    if (response) {
      const token = response.credential;
      console.log('token', token);
      this.authenticateWithServer(token);
    } else {
      this.showMessage('error', 'Đăng nhập băng Google không thành công.');
    }
  }
  authenticateWithServer(token: string) {
    this.accountService.googleLogin({ token: token }).subscribe(
      (user: User) => {
        localStorage.setItem('userInfo', JSON.stringify(user));
        window.location.href = '/';
      },
      () => {
        this.showMessage('error', 'Đăng nhập không thành công.');
      }
    );
  }

  handleGoogleLogin(response: any) {
    if (response.credential) {
      this.handleLogin(response);
    } else {
      this.showMessage('error', 'Đăng nhập bằng Google không thành công.');
    }
  }

  loginWithFacebook() {
    if (!this.sdkReady) {
      this.showMessage(
        'error',
        'SDK Facebook chưa sẵn sàng, vui lòng thử lại sau.'
      );
      return;
    }
    FB.login(
      (response: { authResponse: any }) => {
        if (response.authResponse) {
          const token = response.authResponse.accessToken;
          this.handleFacebookResponse(token);
        } else {
          this.showMessage('error', 'Đăng nhập thất bại');
        }
      },
      { scope: 'email, public_profile' }
    );
  }

  private handleFacebookResponse(token: string) {
    this.accountService.facebookLogin({ token: token }).subscribe(
      (user: User) => {
        localStorage.setItem('userInfo', JSON.stringify(user));
        window.location.href = '/';
      },
      (error) => {
        this.showMessage('error', error);
        this.showMessage('error', 'Đăng nhập không thành công.');
      }
    );
  }
}
