import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { Size } from "../_models/size.module";

@Injectable({
  providedIn: 'root',
})

export class SizeService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addSize(data: Size){
    return this.http.post(this.apiUrl + "/Size/Add", data);
  }
  updateSize(data: Size){
    return this.http.put(this.apiUrl + "/Size/Update", data);
  }
  getAllSizes(){
    return this.http.get<Size[]>(this.apiUrl + "/Size/GetAll");
  }


}
