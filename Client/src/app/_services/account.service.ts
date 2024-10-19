import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { map, pipe } from "rxjs";
import { Login } from "../_models/login.module";
import { environment } from "../../environments/environment";
import { ResetPassword } from "../_models/resetPassword";
import { User } from "../_models/user.module";

@Injectable({ providedIn: 'root' })
export class AccountService {
  private http = inject(HttpClient);
  apiUrl = environment.apiUrl;

  currentUser = signal<User | null>(null);

  register(data: any){
    return this.http.post<User>(this.apiUrl + "/Account/Register", data).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }
  login(data: Login){
    return this.http.post<User>(this.apiUrl + "/Account/Login", data).pipe(
      map((user: User) => {
        if(user){
          console.log(user);
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
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
}
