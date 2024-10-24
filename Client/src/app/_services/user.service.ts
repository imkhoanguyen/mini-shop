import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user.module';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsersWithAdminRole(){
    return this.http.get<User[]>(this.apiUrl + "/User/AdminRoleUsers");
  }
  getUsersWithCustomerRole(){
    return this.http.get<User[]>(this.apiUrl + "/User/CustomerRoleUsers");
  }
}
