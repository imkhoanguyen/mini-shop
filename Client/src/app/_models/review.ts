export interface UserReview {
    id: string;
    fullName: string;
}

export interface ReviewImage {
    id: number;
    url: string;
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