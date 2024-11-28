import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError, map, Observable, pipe, tap, throwError } from 'rxjs';
import { Login } from '../_models/login.module';
import { environment } from '../../environments/environment';
import { ResetPassword } from '../_models/resetPassword';
import { User } from '../_models/user.module';
import { MessageService } from './message.service';
import { PaginatedResult } from '../_models/pagination';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private http = inject(HttpClient);
  private messageService = inject(MessageService);
  private users: string[] = [];
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
  getUsersPagedList(params: any): Observable<PaginatedResult<User[]>> {
    let httpParams = new HttpParams();
    if (params.pageNumber) httpParams = httpParams.set('pageNumber', params.pageNumber.toString());
    if (params.pageSize) httpParams = httpParams.set('pageSize', params.pageSize.toString());
    if (params.orderBy) httpParams = httpParams.set('orderBy', params.orderBy);
    if (params.search) httpParams = httpParams.set('search', params.search);

    return this.http.get<User[]>(this.apiUrl + "/Account/all", {
      params: httpParams,
      observe: 'response'
    }).pipe(
      map((response: HttpResponse<User[]>) => {
        const paginatedResult = new PaginatedResult<User[]>();
        paginatedResult.items = response.body || [];

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }
  addUser(user: FormData): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Account/Add/User', user);
  }
  updateUser(user: FormData): Observable<User> {
    return this.http.put<User>(this.apiUrl + '/Account/Update/User', user);
  }
  lockUser(id: string, lockParams: any) {
    const params = new HttpParams()
      .set('minutes', lockParams.minutes || '')
      .set('hours', lockParams.hours || '')
      .set('days', lockParams.days || '');

    return this.http.put(`http://localhost:5000/api/Account/Lock/User?id=${id}`, null, {
      params,
      responseType: 'text',
    });
  }
  unlockUser(userId: string): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/Account/Unlock/User?id=${userId}`, {});
  }

  getCurrentUser(): User | null {
    const userJson = localStorage.getItem('user');
    if (userJson) {
      try {
        const user: User = JSON.parse(userJson);
        return user;
      } catch (error) {
        console.error('Error parsing user from localStorage', error);
        return null;
      }
    }
    return null; // Trả về null nếu không có user trong localStorage
  }
  getUserId(userId: string){
    return this.http.get<User>(this.apiUrl +"/Account/userId/"+userId);
  }

  isCustomerRole(): Promise<boolean> {
    return new Promise((resolve) => {
      const user = this.getCurrentUser();
      if (!user) {
        resolve(false);
        return;
      }
      this.messageService.getCustomers().subscribe({
        next: (customers: string[]) => {
          resolve(customers.includes(user.id));
        },
        error: () => {
          resolve(false);
        }
      });
    });
  }


}
