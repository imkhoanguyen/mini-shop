import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import {  VariantAdd, VariantDto, VariantUpdate } from "../_models/variant.module";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})

export class VariantService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addVariant(data: FormData): Observable<VariantDto>{
    return this.http.post<VariantDto>(this.apiUrl + "/Variant/Add", data);
  }
  updateVariant(data: FormData): Observable<VariantDto>{
    return this.http.put<VariantDto>(this.apiUrl + "/Variant/Update", data);
  }

  deleteVariant(variantId : number){
    return this.http.delete(this.apiUrl + "/Variant/Delete/?id=" + variantId);
  }

}
