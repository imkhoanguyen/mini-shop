import { Component, OnInit, OnDestroy } from '@angular/core';
import { DiscountService } from '../../../_services/discount.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';
import { Subscription } from 'rxjs';
import { DiscountDto } from '../../../_models/discount.module';
import { ToastrService } from '../../../_services/toastr.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { Pagination } from '../../../_models/pagination';
import { switchMap } from 'rxjs/operators';
import { CalendarModule } from 'primeng/calendar';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';

@Component({
  selector: 'app-discount',
  standalone: true,
  imports: [
    ButtonModule,
    CheckboxModule,
    CommonModule,
    TableModule,
    DropdownModule,
    InputTextModule,
    DialogModule,
    ToastModule,
    ReactiveFormsModule,
    PaginatorModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    CalendarModule,
    IconFieldModule,
    InputIconModule,
  ],
  providers: [ConfirmationService],
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.css'],
})
export class DiscountComponent implements OnInit, OnDestroy {
  isUpdate: boolean = false;
  selectedDiscounts: DiscountDto[] = [];
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Mã Giảm Giá';
  discountForm!: FormGroup;

  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
  ];

  first: number = 0;
  pagination: Pagination = {
    currentPage: 1,
    itemPerPage: 10,
    totalItems: 0,
    totalPages: 1,
  };
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = '';

  private subscriptions: Subscription = new Subscription();

  constructor(
    private builder: FormBuilder,
    private discountService: DiscountService,
    private confirmationService: ConfirmationService,
    private toastService: ToastrService
  ) {
    this.discountForm = this.initializeForm();
  }

  ngOnInit(): void {
    this.loadDiscounts();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  initializeForm(): FormGroup {
    return this.builder.group({
      id: [0],
      name: ['', Validators.required],
      amountOff: ['', [Validators.min(0)]],
      percentOff: ['', [Validators.min(0)]],
      promotionCode: [''],
    });
  }

  loadDiscounts(): void {
    const params = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.searchString,
    };
    this.discountService.getDiscountsPagedList(params).subscribe((result) => {
      this.selectedDiscounts = result.items || [];
      this.pagination = result.pagination ?? {
        currentPage: 1,
        itemPerPage: 10,
        totalItems: 0,
        totalPages: 1,
      };
      this.totalRecords = this.pagination.totalItems;
      this.first = (this.pageNumber - 1) * this.pageSize;
    });
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.pageNumber = event.page + 1;
    this.pageSize = event.rows;

    this.loadDiscounts();
  }

  onPageSizeChange(newPageSize: Event): void {
    const target = newPageSize.target as HTMLSelectElement;
    if (target) {
      this.pageSize = +target.value;
      this.pageNumber = 1;
      this.loadDiscounts();
    }
  }

  onSearch(): void {
    this.pageNumber = 1;
    this.loadDiscounts();
  }

  openDialog(discount?: DiscountDto): void {
    this.visible = true;
    if (discount) {
      this.isUpdate = true;
      this.discountForm.patchValue(discount);
      this.btnText = 'Cập nhật';
      this.headerText = 'Cập nhật Mã Giảm Giá';
    } else {
      this.isUpdate = false;
      this.discountForm.reset();
      this.btnText = 'Thêm';
      this.headerText = 'Thêm Mã Giảm Giá';
    }
  }
  generateCode(name: string): string {
    const characters =
      'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    const length = 8;
    let code = '';
    for (let i = 0; i < length; i++) {
      code += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return code + name;
  }
  onSubmit(): void {
    if (this.discountForm.invalid) {
      this.toastService.error('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    const discountData = {
      ...this.discountForm.value,
      id: this.discountForm.value.id || 0,
    };
    if (discountData.id===0) discountData.promotionCode="00";
    const subscription =
      discountData.id === 0
        ? this.discountService
            .addDiscount(discountData)
            .pipe(
              switchMap(() =>
                this.discountService.getDiscountsPagedList({
                  pageNumber: this.pageNumber,
                  pageSize: this.pageSize,
                })
              )
            )
            .subscribe({
              next: (result) => {
                discountData.promotionCode = this.generateCode(discountData.id);
                this.selectedDiscounts = result.items || [];
                console.log(this.selectedDiscounts);
                console.log(discountData.id);
                this.toastService.success(
                  'Mã giảm giá đã được thêm thành công.'
                );
                this.visible = false;
                this.loadDiscounts();
              },
              error: (err) => {
                this.toastService.error('Có lỗi xảy ra khi thêm mã giảm giá.');
                console.error(err);
              },
            })
        : this.discountService
            .updateDiscount(discountData)
            .pipe(
              switchMap(() =>
                this.discountService.getDiscountsPagedList({
                  pageNumber: this.pageNumber,
                  pageSize: this.pageSize,
                })
              )
            )
            .subscribe({
              next: (result) => {
                this.selectedDiscounts = Array.isArray(result.items)
                  ? result.items
                  : [];
                this.toastService.success(
                  'Mã giảm giá đã được cập nhật thành công.'
                );
                this.visible = false;
                this.loadDiscounts();
              },
              error: (err) => {
                this.toastService.error(
                  'Có lỗi xảy ra khi cập nhật mã giảm giá.'
                );
                console.error(err);
              },
            });

    this.subscriptions.add(subscription);
  }

  confirmDelete(discount: DiscountDto): void {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa mã giảm giá này?',
      accept: () => {
        const subscription = this.discountService
          .deleteDiscount(discount.id)
          .pipe(
            switchMap(() =>
              this.discountService.getDiscountsPagedList({
                pageNumber: this.pageNumber,
                pageSize: this.pageSize,
              })
            )
          )
          .subscribe({
            next: (result) => {
              this.selectedDiscounts = Array.isArray(result.items)
                ? result.items
                : [];
              this.toastService.success('Mã giảm giá đã được xóa thành công.');
              this.loadDiscounts();
            },
            error: (err) => {
              this.toastService.error('Có lỗi xảy ra khi xóa mã giảm giá.');
              console.error(err);
            },
          });

        this.subscriptions.add(subscription);
      },
    });
  }
}
