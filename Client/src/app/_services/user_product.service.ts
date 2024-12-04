import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserProduct } from "../_models/user_product.module";

@Injectable({
  providedIn: 'root',
})
export class UserProductService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient){}

  getFavoriteProducts(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/ProductUserLike/${userId}/favorites`);
  }

  // Add a product to the liked list
  addLikedProduct(userId: string, productId: number): Observable<any> {
    const dto: UserProduct = { userId: userId, productId: productId };
    console.log("Dữ liệu gửi đi:", dto);

    return this.http.post(`${this.apiUrl}/ProductUserLike/liked-products`, dto);
  }

  // Remove a product from the liked list
  removeLikedProduct(userId: string, productId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/ProductUserLike/${userId}/${productId}`);
  }

}
