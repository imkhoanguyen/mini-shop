import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import {
  ReplyCreateDto,
  ReviewDto,
  ReviewEditDto,
  ReviewParams,
} from '../_models/review';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl = environment.apiUrl;
  private http = inject(HttpClient);
  paginatedResult: PaginatedResult<ReviewDto[]> = new PaginatedResult<
    ReviewDto[]
  >();

  getAllReviewByProductId(
    productId: number,
    prm: ReviewParams,
    page?: number,
    itemPerPage?: number
  ) {
    let params = new HttpParams();
    if (page && itemPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }

    if (prm.orderBy) {
      params = params.append('orderBy', prm.orderBy);
    }

    if (prm.rating >= 0) {
      params = params.append('rating', prm.rating.toString());
    }

    return this.http
      .get<ReviewDto[]>(this.apiUrl + `/review/${productId}`, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          if (response.body) this.paginatedResult.items = response.body;

          const pagination = response.headers.get('Pagination');
          if (pagination) {
            this.paginatedResult.pagination = JSON.parse(pagination);
          }

          return this.paginatedResult;
        })
      );
  }

  addReview(frmData: FormData) {
    return this.http.post<ReviewDto>(this.apiUrl + `/review`, frmData);
  }

  updateReview(reviewEditDto: ReviewEditDto) {
    return this.http.put<ReviewDto>(
      this.apiUrl + `/review/${reviewEditDto.id}`,
      reviewEditDto
    );
  }

  addImages(reviewID: number, frmData: FormData) {
    return this.http.post<ReviewDto>(
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
    return this.http.post<ReviewDto>(
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

  canReview(productId: number) {
    return this.http.get<boolean>(
      `${this.apiUrl}/review/check-permission/${productId}`
    );
  }

  getTotal(productId: number) {
    return this.http.get<number>(
      `${this.apiUrl}/review/total-rating/${productId}`
    );
  }
}
