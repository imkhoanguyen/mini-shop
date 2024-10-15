import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { Register } from "../_models/register.module";
import { Login } from "../_models/login.module";

@Injectable({
  providedIn: 'root'
})
export class AccountService{
  constructor(private http: HttpClient){}
  apiUrl = environment.apiUrl;

  register(data: Register){
    return this.http.post(this.apiUrl + "/Account/Register", data);
  }
  login(data: Login){
    return this.http.post(this.apiUrl + "/Account/Login", data);
  }
}

