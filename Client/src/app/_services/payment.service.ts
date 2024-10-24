import { HttpClient } from "@angular/common/http";
import { Injectable, signal } from "@angular/core";
import { Payment } from "../_models/payment.module";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable ({
    providedIn: 'root'
})
export class PaymentService 
{
    constructor(private http: HttpClient){}
    paymetList=signal<Payment[]>([]);
    paymentItems=signal<Payment>({
        id:0,
        payment_method: "",
        status: 0,
        payment_date: new Date()
    });
    apiUrl=environment.apiUrl;
    addPayment(data: Payment): Observable<Payment>{
        return this.http.post<Payment>(this.apiUrl+"/Payment/Add",data);
    }
    updatePayment(data: Payment): Observable<Payment>{
        return this.http.put<Payment>(this.apiUrl + "/Payment/Update",data);
    }
    deletePayment(data: Payment):Observable<Payment>{
        return this.http.delete<Payment>(this.apiUrl+"/Payment/Delete",{body:data});
    }
    getAllPayment(){
        return this.http.get(this.apiUrl+"Payment/GetAll");
    }
}