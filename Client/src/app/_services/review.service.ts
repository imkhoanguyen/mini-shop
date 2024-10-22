import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ReplyCreateDto, ReviewDto, ReviewEditDto } from '../_models/review';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = environment.apiUrl;
  private http = inject(HttpClient);

  getAllReviewByProductId(productId: number) {
    return this.http.get<ReviewDto[]>(this.apiUrl + `/review/${productId}`);
  }

  addReview(frmData: FormData) {
    return this.http.post<ReviewDto>(this.apiUrl + `/review`, frmData);
  }

  updateReview(reviewEditDto: ReviewEditDto) {
    return this.http.put(
      this.apiUrl + `/review/${reviewEditDto.id}`,
      reviewEditDto
    );
  }

  addImages(reviewID: number, frmData: FormData) {
    return this.http.post(
      `${this.apiUrl}/review/add-images/${reviewID}`,
      frmData
    );
  }

  removeImage(reviewId: number, imageId: number) {
    return this.http.delete(
      `${this.apiUrl}/review/remove-image/${reviewId}?imageId=${imageId}`
    );
  }

  addVideo(reviewId: number, frmData: FormData) {
    return this.http.post(
      `${this.apiUrl}/review/add-video/${reviewId}`,
      frmData
    );
  }

  removeVideo(reviewId: number) {
    return this.http.delete(`${this.apiUrl}/review/remove-video/${reviewId}`);
  }

  addReply(reply: ReplyCreateDto) {
    return this.http.post(`${this.apiUrl}/review/add-reply`, reply);
  }

  deleteReview(reviewId: number) {
    return this.http.delete(`${this.apiUrl}/review/${reviewId}`);
  }
}
