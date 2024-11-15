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
  updateVariant(data: VariantUpdate): Observable<VariantDto>{
    return this.http.put<VariantDto>(this.apiUrl + "/Variant/Update", data);
  }
  deleteVariant(variantId : number){
    return this.http.delete(this.apiUrl + "/Variant/Delete/?id=" + variantId);
  }
  getVariantById(variantId: number){
    return this.http.get<VariantDto>(this.apiUrl + "/Variant/"+variantId);
  }
  getVariantByProductId(productId: number){
    return this.http.get<VariantDto[]>(this.apiUrl + "/Variant/GetByProductId?productId=" + productId);
  }
  addVariantImages(variantId: number, files: File[]){
    const formData = new FormData();
    files.forEach(file => {
        formData.append('imageFiles', file);
    });
    return this.http.post(this.apiUrl + "/Variant/add-images/" + variantId, formData);
  }
  removeVariantImage(variantId: number, imageId: number){
    return this.http.delete(this.apiUrl + "/Variant/remove-image/" + variantId + "/?imageId=" + imageId);
  }

}
