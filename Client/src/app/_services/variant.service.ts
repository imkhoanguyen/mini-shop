import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Variant } from "../_models/variant.module";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root',
})

export class VariantService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addVariant(data: Variant){
    return this.http.post(this.apiUrl + "/Variant/AddVariant", data);
  }
  updateVariant(data: Variant){
    return this.http.put(this.apiUrl + "/Variant/UpdateVariant", data);
  }


}
