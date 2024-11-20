import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { Color } from "../_models/color.module";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})

export class ColorService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addColor(data: Color){
    return this.http.post(this.apiUrl + "/Color/Add", data);
  }
  updateColor(data: Color){
    return this.http.put(this.apiUrl + "/Color/Update", data);
  }
  getAllColors(){
    return this.http.get<Color[]>(this.apiUrl + "/Color/GetAll");
  }
  getColorById(id: number){
    return this.http.get<Color>(this.apiUrl + "/Color/GetById/" + id);
  }
}
