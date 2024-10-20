import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError, map, Observable, pipe, tap, throwError } from 'rxjs';
import { Login } from '../_models/login.module';
import { environment } from '../../environments/environment';
import { ResetPassword } from '../_models/resetPassword';
import { User } from '../_models/user.module';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  currentUser = signal<User | null>(null);

  register(data: any) {
    return this.http.post<User>(this.apiUrl + '/Account/Register', data).pipe(
      tap((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      }),
      catchError((error) => {
        return throwError(
          () => new Error(error.error.errors[0] || 'Registration failed')
        );
      })
    );
  }
  login(data: Login) {
    return this.http.post<User>(this.apiUrl + '/Account/Login', data).pipe(
      tap((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      }),
      catchError((error) => {
        return throwError(
          () => new Error(error.error.message || 'Login failed')
        );
      })
    );
  }
  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.currentUser.set(null);
  }
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }
  forgotPassword(email: string) {
    return this.http.get(
      this.apiUrl + `/account/forget-password?email=${email}`
    );
  }
  resetPassword(resetPassword: ResetPassword) {
    return this.http.post(
      this.apiUrl + '/account/reset-password',
      resetPassword
    );
  }

  googleLogin(data: { token: string }): Observable<User> {
    {
      return this.http
        .post<User>(this.apiUrl + '/Account/login/google', data)
        .pipe(
          tap((user) => {
            if (user) {
              this.setCurrentUser(user);
            }
          }),
          catchError((error) => {
            return throwError(
              () => new Error(error.error.message || 'Login failed')
            );
          })
        );
    }
  }

  facebookLogin(data: { token: string }): Observable<User> {
    {
      return this.http
        .post<User>(this.apiUrl + '/Account/login/facebook', data)
        .pipe(
          tap((user) => {
            if (user) {
              this.setCurrentUser(user);
            }
          }),
          catchError((error) => {
            return throwError(
              () => new Error(error.error.message || 'Login failed')
            );
          })
        );
    }
  }
}
