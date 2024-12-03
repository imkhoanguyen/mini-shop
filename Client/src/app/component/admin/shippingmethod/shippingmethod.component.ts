import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShippingMethodService } from '../../../_services/shippingMethod.service';
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
import { ShippingMethodDto } from '../../../_models/shippingMethod.module';
import { ToastrService } from '../../../_services/toastr.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { Pagination } from '../../../_models/pagination';
import { switchMap } from 'rxjs/operators';
import { CalendarModule } from 'primeng/calendar';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
@Component({
  selector: 'app-shippingmethod',
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
  templateUrl: './shippingmethod.component.html',
  styleUrls: ['./shippingmethod.component.css'],
})
export class ShippingMethodComponent implements OnInit, OnDestroy {
  selectedShippingMethods: ShippingMethodDto[] = [];
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Phương Thức';
  shippingMethodForm!: FormGroup;

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
    private shippingMethodService: ShippingMethodService,
    private confirmationService: ConfirmationService,
    private toastService: ToastrService
  ) {
    this.shippingMethodForm = this.initializeForm();
  }
  convertToUTC7(date: Date | string | null): Date | null {
    if (!date) return null;
    const convertedDate = new Date(date);
    convertedDate.setHours(convertedDate.getHours() + 7); // Cộng 7 giờ
    return convertedDate;
  }
  ngOnInit(): void {
    this.loadShippingMethods();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  initializeForm(): FormGroup {
    return this.builder.group({
      id: [0],
      name: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]],
      estimatedDeliveryTime: ['', [Validators.required]],
    });
  }

  loadShippingMethods(): void {
    const params = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.searchString,
    };
    this.shippingMethodService
      .getShippingMethodsPagedList(params)
      .subscribe((result) => {
        this.selectedShippingMethods = result.items || [];
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

    this.loadShippingMethods();
  }

  onPageSizeChange(newPageSize: Event): void {
    const target = newPageSize.target as HTMLSelectElement;
    if (target) {
      this.pageSize = +target.value;
      this.pageNumber = 1;
      this.loadShippingMethods();
    }
  }

  onSearch(): void {
    this.pageNumber = 1;
    console.log('search', this.searchString);
    this.loadShippingMethods();
  }

  openDialog(shippingMethod?: ShippingMethodDto): void {
    this.visible = true;
    if (shippingMethod) {
      this.shippingMethodForm.patchValue(shippingMethod);
      this.btnText = 'Cập nhật';
      this.headerText = 'Cập nhật Phương Thức';
    } else {
      this.shippingMethodForm.reset();
      this.btnText = 'Thêm';
      this.headerText = 'Thêm Phương Thức';
    }
  }

  onSubmit(): void {
    if (this.shippingMethodForm.invalid) {
      this.toastService.error('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    const shippingMethodData = {
      ...this.shippingMethodForm.value,
      id: this.shippingMethodForm.value.id || 0,
    };

    const subscription =
      shippingMethodData.id === 0
        ? this.shippingMethodService
            .addShippingMethod(shippingMethodData)
            .pipe(
              switchMap(() =>
                this.shippingMethodService.getShippingMethodsPagedList({
                  pageNumber: this.pageNumber,
                  pageSize: this.pageSize,
                })
              )
            )
            .subscribe({
              next: (result) => {
                this.selectedShippingMethods = result.items || [];
                this.toastService.success(
                  'Phương thức đã được thêm thành công.'
                );
                this.visible = false;
                this.loadShippingMethods();
              },
              error: (err) => {
                this.toastService.error('Có lỗi xảy ra khi thêm phương thức.');
                console.error(err);
              },
            })
        : this.shippingMethodService
            .updateShippingMethod(shippingMethodData)
            .pipe(
              switchMap(() =>
                this.shippingMethodService.getShippingMethodsPagedList({
                  pageNumber: this.pageNumber,
                  pageSize: this.pageSize,
                })
              )
            )
            .subscribe({
              next: (result) => {
                this.selectedShippingMethods = Array.isArray(result.items)
                  ? result.items
                  : [];
                this.toastService.success(
                  'Phương thức đã được cập nhật thành công.'
                );
                this.visible = false;
                this.loadShippingMethods();
              },
              error: (err) => {
                this.toastService.error(
                  'Có lỗi xảy ra khi cập nhật phương thức.'
                );
                console.error(err);
              },
            });

    this.subscriptions.add(subscription);
  }

  confirmDelete(shippingMethod: ShippingMethodDto): void {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa phương thức này?',
      accept: () => {
        const subscription = this.shippingMethodService
          .deleteShippingMethod(shippingMethod.id)
          .pipe(
            switchMap(() =>
              this.shippingMethodService.getShippingMethodsPagedList({
                pageNumber: this.pageNumber,
                pageSize: this.pageSize,
              })
            )
          )
          .subscribe({
            next: (result) => {
              this.selectedShippingMethods = Array.isArray(result.items)
                ? result.items
                : [];
              this.toastService.success('Phương thức đã được xóa thành công.');
              this.loadShippingMethods();
            },
            error: (err) => {
              this.toastService.error('Có lỗi xảy ra khi xóa phương thức.');
              console.error(err);
            },
          });

        this.subscriptions.add(subscription);
      },
    });
  }
}
