import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Voucher } from "../_models/voucher.module";

@Injectable(
{
  providedIn: 'root'
})

export class VoucherService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}
  getVoucherById(id: number){
    return this.http.get<Voucher>(`${this.apiUrl}/Voucher/GetVoucher/${id}`);
  }
  getAllVoucher(){
    return this.http.get<Voucher[]>(this.apiUrl + "/Voucher/GetAllVouchers");
  }
  addVoucher(data: FormData){
    return this.http.post(`${this.apiUrl}/Voucher/AddVoucher`, data);
  }
  updateVoucher(id: number,data:FormData){
    return this.http.put(`${this.apiUrl}/Voucher/UpdateVoucher/${id}`, data);
  }

  deleteVoucher(id: number){
    return this.http.delete(`${this.apiUrl}/Voucher/DeleteVoucher/${id}`);
  }
  restoreVoucher(id: number){
    return this.http.delete(`${this.apiUrl}/Voucher/RestoreVoucher/${id}`);
  }
  
  
}

