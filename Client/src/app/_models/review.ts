export interface UserReview {
  id: string;
  fullName: string;
  imgUrl: string;
}

export interface ReviewImage {
  id: number;
  imgUrl: string;
}

export interface ReviewDto {
  id: number;
  reviewText: string;
  rating?: number | null;
  created: string;
  parentReviewId?: number | null;
  videoUrl?: string | null;
  userReview: UserReview;
  productId: number;
  replies: ReviewDto[];
  images: ReviewImage[];
}

export interface ReviewEditDto {
  id: number;
  rating?: number | null;
  reviewText: string;
}

export interface ReplyCreateDto {
  reviewText: string;
  parentReviewId: number;
  userId: string;
  productId: number;
}

export class ReviewParams {
  orderBy: string = 'id_desc';
  rating: number = 0;
}
