import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Variant, VariantAdd } from "../_models/variant.module";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})

export class VariantService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addVariant(data: VariantAdd): Observable<any>{
    return this.http.post(this.apiUrl + "/Variant/AddVariant", data);
  }
  updateVariant(data: Variant){
    return this.http.put(this.apiUrl + "/Variant/UpdateVariant", data);
  }

  deleteVariant(data: Variant){
    return this.http.delete(this.apiUrl + "/Variant/DeleteVariant/", {body: data});
  }

}
