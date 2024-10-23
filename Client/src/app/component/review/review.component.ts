import {
  Component,
  inject,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  ReplyCreateDto,
  ReviewDto,
  ReviewEditDto,
  ReviewImage,
} from '../../_models/review';
import { ReviewService } from '../../_services/review.service';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FileUpload, FileUploadModule } from 'primeng/fileupload';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { AccountService } from '../../_services/account.service';
import { TabViewModule } from 'primeng/tabview';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { SignalrService } from '../../_services/signalr.service';
import { Subject, Subscription } from 'rxjs'; // Correct import from RxJS
@Component({
  selector: 'app-review',
  standalone: true,
  imports: [
    ToastModule,
    CommonModule,
    RatingModule,
    FormsModule,
    DialogModule,
    ReactiveFormsModule,
    FileUploadModule,
    TabViewModule,
  ],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css',
  providers: [MessageService],
})
export class ReviewComponent implements OnInit, OnDestroy {
  @Input() productId: number = 0;

  reviews: ReviewDto[] = [];
  showReplies: { [key: number]: boolean } = {};
  showDropdown: { [key: number]: boolean } = {};
  showReplyDropdown: { [reviewId: number]: { [replyId: number]: boolean } } =
    {};

  visibleFrmCreateView = false;
  imgFiles: any[] = [];
  videoUrl: string = '';
  private accountServices = inject(AccountService);
  currentUser = this.accountServices.getCurrentUser();
  private fb = inject(FormBuilder);

  private reviewServices = inject(ReviewService);
  private signalRService = inject(SignalrService);
  private subscription: Subscription;
  constructor(private messageService: MessageService) {
    this.subscription = new Subscription();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit(): void {
    this.loadReviews();
    this.initFormCreateView();
    this.initFrmEdit();
    this.initFormCreateReply();
    this.signalRService.startConnection();

    const reviewSubscription = this.signalRService.reviewReceived.subscribe(
      (review: ReviewDto) => {
        this.reviews.unshift(review);
      }
    );

    const updateReviewSubscription =
      this.signalRService.reviewUpdated.subscribe((reviewDto: ReviewDto) => {
        const index = this.reviews.findIndex((r) => r.id === reviewDto.id);
        if (index !== -1) {
          this.reviews[index] = reviewDto;
        } else {
          const parentReviewId = reviewDto.parentReviewId;
          const parentReview = this.reviews.find(
            (review) => review.id === parentReviewId
          );

          if (parentReview) {
            const replyIndex = parentReview.replies.findIndex(
              (reply) => reply.id === reviewDto.id
            );

            if (replyIndex !== -1) {
              parentReview.replies[replyIndex] = reviewDto;
            }
          }
        }
      });

    const deleteReviewSubscription =
      this.signalRService.reviewDeleted.subscribe((reviewId: number) => {
        console.log('reviewId', reviewId);
        const index = this.reviews.findIndex((r) => r.id === reviewId);
        if (index !== -1)
          this.reviews = this.reviews.filter((r) => r.id !== reviewId);
        else
          this.reviews.forEach((review) => {
            review.replies = review.replies.filter(
              (reply) => reply.id !== reviewId
            );
          });
      });

    const addReplySubscription = this.signalRService.replyReceived.subscribe(
      (reviewDto: ReviewDto) => {
        const parentReviewId = reviewDto.parentReviewId;

        const parentReview = this.reviews.find(
          (review) => review.id === parentReviewId
        );

        if (parentReview) {
          parentReview.replies.unshift(reviewDto);
        }
      }
    );
    this.subscription.add(reviewSubscription);
    this.subscription.add(updateReviewSubscription);
    this.subscription.add(deleteReviewSubscription);
    this.subscription.add(addReplySubscription);
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

  onDeleteReview(reviewId: number) {
    this.reviewServices.deleteReview(reviewId).subscribe({
      next: (_) => {
        this.loadReviews();
        this.showSuccess('Xóa đánh giá thành công');
      },
      error: (er) => {
        console.log(er);
        this.showError(er);
      },
    });
  }

  showError(detail: string, summary?: string) {
    this.messageService.add({
      severity: 'error',
      summary: summary || 'Error',
      detail: detail,
      life: 3000,
    });
  }

  showSuccess(detail: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: detail,
      life: 3000,
    });
  }

  // ********************************************** Start Create Form Review & Reply **********************************************

  @ViewChild('imageUpload') imageFileUpload!: FileUpload;
  @ViewChild('videoUpload') videoFileUpload!: FileUpload;

  frmCreateReview: FormGroup = new FormGroup({});

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
    this.frmCreateReview.reset();
    this.imgFiles = [];
    this.videoUrl = '';
    this.imageFileUpload.clear();
    this.videoFileUpload.clear();
  }

  onSelectImgFile(event: any) {
    const files = event.currentFiles;

    files.forEach((file: File) => {
      if (
        !this.imgFiles.some(
          (existingFile: File) => existingFile.name === file.name
        )
      ) {
        this.imgFiles.push(file);
      }
    });

    this.frmCreateReview.patchValue({
      imageFile: this.imgFiles,
    });
    this.frmCreateReview.get('files')?.updateValueAndValidity();

    console.log(this.imgFiles);
    console.log('current form value: ', this.frmCreateReview.value);
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
    if (
      this.frmCreateReview.value.imageFile &&
      this.frmCreateReview.value.imageFile.length > 0
    ) {
      for (let i = 0; i < this.frmCreateReview.value.imageFile.length; i++) {
        formData.append('imageFile', this.frmCreateReview.value.imageFile[i]);
      }
    }

    if (this.currentUser) {
      formData.append('UserId', this.currentUser.id.toString());
    }

    if (this.productId > 0) {
      formData.append('productId', this.productId.toString());
    }
    console.log(this.currentUser);

    this.reviewServices.addReview(formData).subscribe({
      next: (response) => {
        this.loadReviews();
        this.showSuccess('Thêm đánh giá thành công');
        this.closeFormCreateReview();
      },
      error: (er) => {
        console.log(er);
      },
    });
  }

  // ********************************************** End Create Form Review **********************************************

  // ********************************************** Start Edit Form Review **********************************************
  @ViewChild('imageUploadEdit') imageFileUploadEdit!: FileUpload;
  @ViewChild('videoUploadEdit') videoFileUploadEdit!: FileUpload;
  frmEditReview: FormGroup = new FormGroup({});

  visibleFrmEditView: boolean = false;
  imgListForEdit: ReviewImage[] = []; // read-only
  videoUrlForEdit: string | null = ''; // read-only
  listImgRequest: File[] = [];
  videoRequest: File | null = null;
  showAddVideoBox = false;

  initFrmEdit() {
    this.frmEditReview = this.fb.group({
      id: 0,
      reviewText: new FormControl<string>('', [Validators.required]),
      rating: new FormControl<number>(5, [Validators.required]),
    });
  }

  openFormEditReview(reviewId: number) {
    const review = this.reviews.find((r) => r.id === reviewId);
    if (review != null) {
      this.frmEditReview.patchValue({
        id: reviewId,
        reviewText: review?.reviewText,
        rating: review?.rating,
      });
      this.imgListForEdit = review.images;
      this.videoUrlForEdit = review.videoUrl || null;
      if (this.videoUrlForEdit == null) this.showAddVideoBox = true;
    }

    this.visibleFrmEditView = true;
  }

  onSubmitFrmEditReview() {
    const reviewEditDto: ReviewEditDto = {
      id: this.frmEditReview.value.id,
      reviewText: this.frmEditReview.value.reviewText,
      rating: this.frmEditReview.value.rating,
    };

    this.reviewServices.updateReview(reviewEditDto).subscribe({
      next: (_) => {
        this.showSuccess('Cập nhật đánh giá thành công');
        this.loadReviews();
      },
    });
  }

  closeFormEditReview() {
    this.frmEditReview.reset();
    // img reset
    this.imgListForEdit = [];
    this.listImgRequest = [];
    // video reset
    this.videoUrlForEdit = '';
    this.videoRequest = null;
    this.showAddVideoBox = false;
    //
    this.imageFileUploadEdit.clear();
    if (this.videoUrlForEdit !== '') this.videoFileUploadEdit.clear();
    this.visibleFrmEditView = false;
  }

  onSelectImgFileEdit(event: any) {
    const files = event.currentFiles;

    files.forEach((file: File) => {
      if (
        !this.listImgRequest.some(
          (existingFile: File) => existingFile.name === file.name
        )
      ) {
        this.listImgRequest.push(file);
      }
    });

    console.log(this.listImgRequest);
  }

  onRemoveImgFileEdit(event: any) {
    const fileToRemove = event.file;

    this.listImgRequest = this.listImgRequest.filter(
      (file) => file !== fileToRemove
    );

    console.log('Current img list request after remove:', this.listImgRequest);
  }

  onClearAllImageFilesEdit(event: any) {
    this.listImgRequest = [];
    console.log('Current img list request after clear:', this.listImgRequest);
  }

  onUploadImgEdit() {
    let frmData = new FormData();
    if (this.listImgRequest && this.listImgRequest.length > 0) {
      for (let i = 0; i < this.listImgRequest.length; i++) {
        frmData.append('imageFiles', this.listImgRequest[i]);
      }
    }
    console.log(frmData);
    this.reviewServices
      .addImages(this.frmEditReview.value.id, frmData)
      .subscribe({
        next: (_) => {
          this.showSuccess('Thêm ảnh đánh giá sản phẩm thành công');
          this.loadReviews();
          this.closeFormEditReview();
        },
        error: (er) => {
          console.log(er);
          this.showError('Thêm ảnh đánh giá sản phẩm thất bại');
        },
      });
  }

  removeImage(imgId: number) {
    this.reviewServices
      .removeImage(this.frmEditReview.value.id, imgId)
      .subscribe({
        next: (_) => {
          this.showSuccess('Xóa hình ảnh thành công');
          this.loadReviews();
          this.closeFormEditReview();
        },
        error: (er) => {
          console.log(er);
          this.showError('Xóa hình ảnh thất bại');
        },
      });
  }

  // video file
  onSelectVideoFileEdit(event: any) {
    const file = event.files[0];

    if (file && file.type.startsWith('video/')) {
      this.videoRequest = file;

      const fileReader = new FileReader();
      fileReader.onload = (e: any) => {
        this.videoUrlForEdit = e.target.result; // Hiển thị URL của video để preview
      };
      fileReader.readAsDataURL(file); // Đọc tệp để tạo URL hiển thị trước
    } else {
      console.log('Invalid file format. Please select a valid video file.');
      this.onRemoveVideoFileEdit();
    }
    console.log('Current form value:', this.frmCreateReview.value);
  }

  onRemoveVideoFileEdit() {
    this.videoRequest = null;
    this.videoUrlForEdit = '';
    console.log(
      'current videoUrl after remove',
      this.videoRequest,
      this.videoUrlForEdit
    );
  }

  onUploadVideoEdit() {
    let formData = new FormData();
    if (this.videoRequest != null) {
      formData.append('videoFile', this.videoRequest);
      this.reviewServices
        .addVideo(this.frmEditReview.value.id, formData)
        .subscribe({
          next: (_) => {
            this.showSuccess('Thêm video thành công');
            this.loadReviews();
            this.closeFormEditReview();
          },
        });
    } else {
      this.showError('Chưa có video');
    }
  }

  removeVideo() {
    this.reviewServices.removeVideo(this.frmEditReview.value.id).subscribe({
      next: (_) => {
        this.showSuccess('Xóa video thành công');
        this.closeFormEditReview();
        this.loadReviews();
      },
      error: (er) => {
        console.log(er);
        this.showError(er);
      },
    });
  }

  // ********************************************** End Edit Form Review **********************************************

  // ********************************************** Start Create Form Reply **********************************************
  frmCreateReply: FormGroup = new FormGroup({});
  visibleFrmCreateReply = false;
  parentId = 0;
  editReply = false;
  reviewIdForEditReply = 0;
  initFormCreateReply() {
    this.frmCreateReply = this.fb.group({
      reviewText: new FormControl<string>('', [Validators.required]),
    });
  }
  closeFormCreateReply() {
    this.visibleFrmCreateReply = false;
    this.frmCreateReply.reset();
    this.parentId = 0;
    this.editReply = false;
    this.reviewIdForEditReply = 0;
  }

  openFormCreateReply(reviewId: number, reviewText?: string) {
    this.visibleFrmCreateReply = true;
    this.parentId = reviewId;
    if (reviewText) {
      this.editReply = true;
      this.frmCreateReply.patchValue({
        reviewText: reviewText,
      });
      this.reviewIdForEditReply = reviewId;
    }
  }

  onSubmitFrmCreateReply() {
    if (!this.currentUser) {
      this.showError('Bạn chưa đăng nhập');
      return;
    }

    if (this.editReply) {
      const reply: ReviewEditDto = {
        id: this.reviewIdForEditReply,
        reviewText: this.frmCreateReply.value.reviewText,
        rating: null,
      };

      this.reviewServices.updateReview(reply).subscribe({
        next: (_) => {
          this.showSuccess('Cập nhật phản hồi thành công');
          this.loadReviews();
          this.closeFormCreateReply();
        },
        error: (er) => {
          console.log(er), this.showError('Cập nhật phản hồi thất bại');
        },
      });
    } else {
      const reply: ReplyCreateDto = {
        reviewText: this.frmCreateReply.value.reviewText,
        parentReviewId: this.parentId,
        userId: this.currentUser?.id.toString(),
        productId: this.productId,
      };

      this.reviewServices.addReply(reply).subscribe({
        next: (_) => {
          this.showSuccess('Thêm phản hồi thành công');
          this.loadReviews();
          this.closeFormCreateReply();
        },
        error: (er) => {
          console.log(er);
          this.showError(er);
        },
      });
    }
  }
  // ********************************************** End Create Form Reply **********************************************
}
