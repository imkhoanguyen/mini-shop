import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ReviewDto } from '../_models/review';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = environment.apiUrl;
  private http = inject(HttpClient);

  getAllReviewByProductId(productId: number) {
    return this.http.get<ReviewDto[]>(this.apiUrl + `/review/${productId}`);
  }
}
