import { Component, inject, Input, OnInit } from '@angular/core';
import { ReviewDto } from '../../_models/review';
import { ReviewService } from '../../_services/review.service';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FileUploadModule } from 'primeng/fileupload';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [
    CommonModule,
    RatingModule,
    FormsModule,
    DialogModule,
    ReactiveFormsModule,
    FileUploadModule,
  ],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css',
})
export class ReviewComponent implements OnInit {
  @Input() productId!: number;
  reviews: ReviewDto[] = [];
  showReplies: { [key: number]: boolean } = {};
  showDropdown: { [key: number]: boolean } = {};
  showReplyDropdown: { [reviewId: number]: { [replyId: number]: boolean } } =
    {};

  visibleFrmCreateView = false;
  imgFiles: any[] = [];
  videoUrl: string = '';

  private fb = inject(FormBuilder);

  frmCreateReview: FormGroup = new FormGroup({});

  private reviewServices = inject(ReviewService);

  ngOnInit(): void {
    this.loadReviews();
    this.initFormCreateView();
  }

  loadReviews() {
    this.reviewServices.getAllReviewByProductId(this.productId).subscribe({
      next: (reviews) => {
        this.reviews = reviews;
      },
      error: (error) => {
        console.log('Error loading reviews:', error);
      },
    });
  }

  toggleReplies(reviewId: number): void {
    this.showReplies[reviewId] = !this.showReplies[reviewId];
  }

  toggleDropdown(reviewId: number) {
    this.showDropdown[reviewId] = !this.showDropdown[reviewId];
  }

  toggleReplyDropdown(reviewId: number, replyId: number) {
    if (!this.showReplyDropdown[reviewId]) {
      this.showReplyDropdown[reviewId] = {};
    }
    this.showReplyDropdown[reviewId][replyId] =
      !this.showReplyDropdown[reviewId][replyId];
  }

  edit(reviewId: number) {
    console.log(`Edit review with ID: ${reviewId}`);
  }

  delete(reviewId: number) {
    console.log(`Delete review with ID: ${reviewId}`);
  }

  editReply(reviewId: number, replyId: number) {
    console.log(
      `Edit reply with ID: ${replyId} for review with ID: ${reviewId}`
    );
  }

  deleteReply(reviewId: number, replyId: number) {
    console.log(
      `Delete reply with ID: ${replyId} for review with ID: ${reviewId}`
    );
  }

  // ********************************************** Start Create Form Review **********************************************

  initFormCreateView() {
    this.frmCreateReview = this.fb.group({
      reviewText: new FormControl<string>('', [Validators.required]),
      rating: new FormControl<number>(5, [Validators.required]),
      videoFile: new FormControl<string>(''),
      imageFile: new FormControl<string[]>([]),
    });
  }

  openFormCreateReview() {
    this.visibleFrmCreateView = true;
  }

  closeFormCreateReview() {
    this.visibleFrmCreateView = false;
  }

  onSelectImgFile(event: any) {
    const files = event.currentFiles;
    this.imgFiles.push(...files);

    this.frmCreateReview.patchValue({
      imageFile: this.imgFiles,
    });
    this.frmCreateReview.get('files')?.updateValueAndValidity();

    console.log(this.imgFiles);
  }

  onRemoveImgFile(event: any) {
    const fileToRemove = event.file;

    this.imgFiles = this.imgFiles.filter((file) => file !== fileToRemove);

    this.frmCreateReview.patchValue({
      imageFile: this.imgFiles,
    });
    this.frmCreateReview.get('imageFile')?.updateValueAndValidity();
    console.log('Current form value:', this.frmCreateReview.value);
  }

  onClearAllImageFiles(event: any) {
    this.imgFiles = [];

    this.frmCreateReview.patchValue({
      imageFile: this.imgFiles,
    });
    this.frmCreateReview.get('imageFile')?.updateValueAndValidity();

    console.log(
      'Current form value after clearing files:',
      this.frmCreateReview.value
    );
  }

  onSelectVideoFile(event: any) {
    const file = event.files[0];

    if (file && file.type.startsWith('video/')) {
      this.frmCreateReview.patchValue({
        videoFile: file,
      });

      const fileReader = new FileReader();
      fileReader.onload = (e: any) => {
        this.videoUrl = e.target.result; // Hiển thị URL của video để preview
      };
      fileReader.readAsDataURL(file); // Đọc tệp để tạo URL hiển thị trước
    } else {
      console.log('Invalid file format. Please select a valid video file.');
      this.onRemoveVideoFile();
    }
    console.log('Current form value:', this.frmCreateReview.value);
  }

  onRemoveVideoFile() {
    this.videoUrl = '';
    this.frmCreateReview.patchValue({
      videoFile: '',
    });
    console.log('Current form value:', this.frmCreateReview.value);
  }

  onSubmitFrmCreateReview() {
    const formData = new FormData();
    formData.append('reviewText', this.frmCreateReview.value.reviewText);
    formData.append('rating', this.frmCreateReview.value.rating);

    if (this.frmCreateReview.value.videoFile) {
      formData.append('videoFile', this.frmCreateReview.value.videoFile);
    }

    if (this.frmCreateReview.value.imageFile) {
      formData.append('imgFile', this.frmCreateReview.value.imageFile);
    }

    if(this.frmCreateReview)
  }

  // ********************************************** End Create Form Review **********************************************
}
