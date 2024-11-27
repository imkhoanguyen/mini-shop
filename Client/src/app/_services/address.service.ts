import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { Color } from "../_models/color.module";
import { Address } from "../_models/address.module";

@Injectable({
  providedIn: 'root',
})

export class AddressService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  addAddress(data: Address){
    return this.http.post(this.apiUrl + "/Address/add", data);
  }
  updateAddress(addressId:number,data: Address){
    return this.http.put(this.apiUrl + `/Address/update/${addressId}`, data);
  }
  deleteAddress(addressId:number){
    return this.http.delete(this.apiUrl+ `/Address/delete/${addressId}`)
  }
  getAllAddressByUserId(userId:number){
    return this.http.get<Address[]>(this.apiUrl + `/Address/user/${userId}`);
  }
  getAddressById(id: number){
    return this.http.get<Address>(this.apiUrl + "/Address/" + id);
  }
}
