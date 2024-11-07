import { Component, OnInit, OnDestroy } from '@angular/core';
import { CategoryService } from '../../../_services/category.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';
import { Subscription } from 'rxjs';
import { CategoryDto } from '../../../_models/category.module';
import { ToastrService } from '../../../_services/toastr.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PaginatedResult, Pagination } from '../../../_models/pagination';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-categorylist',
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
    ProgressSpinnerModule
  ],
  providers: [ConfirmationService],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit, OnDestroy {
  selectedCategories!: CategoryDto[];
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Danh Mục';
  categoryForm!: FormGroup;

  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];

  first: number = 0;
  pagination: Pagination = { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = "";

  private subscriptions: Subscription = new Subscription();

  constructor(
    private builder: FormBuilder,
    private categoryService: CategoryService,
    private confirmationService: ConfirmationService,
    private toastService: ToastrService,
  ) {
    this.categoryForm = this.initializeForm();
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  initializeForm(): FormGroup {
    return this.builder.group({
      id: [0],
      name: ['', Validators.required],
    });
  }

  loadCategories(): void {
    const params = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.searchString
    };
    this.categoryService.getCategoriesPagedList(params).subscribe((result) => {
      this.selectedCategories = result.items || [];
      this.pagination = result.pagination ?? { currentPage: 1, itemPerPage: 10, totalItems: 0, totalPages: 1 };

      this.totalRecords = this.pagination.totalItems;
      this.first = (this.pageNumber - 1) * this.pageSize;
    });
  }

  onPageChange(event: any): void {
    this.first = event.first;
    this.pageNumber = event.page + 1;
    this.pageSize = event.rows;


    this.loadCategories();
  }

  onPageSizeChange(newPageSize: any): void {
    this.pageSize = newPageSize.value;
    this.pageNumber = 1;
    this.loadCategories();
  }

  onSearch(): void {
    this.pageNumber = 1;
    console.log("search", this.searchString)
    this.loadCategories();
  }

  openDialog(category?: CategoryDto): void {
    this.visible = true;
    if (category) {
      this.categoryForm.patchValue(category);
      this.btnText = 'Cập nhật';
      this.headerText = 'Cập nhật Danh Mục';
    } else {
      this.categoryForm.reset();
      this.btnText = 'Thêm';
      this.headerText = 'Thêm Danh Mục';
    }
  }

  onSubmit(): void {
    if (this.categoryForm.invalid) {
      this.toastService.error('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    const categoryData = { ...this.categoryForm.value, id: this.categoryForm.value.id || 0 };

    const subscription = categoryData.id === 0
      ? this.categoryService.addCategory(categoryData).pipe(
          switchMap(() => this.categoryService.getCategoriesPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedCategories = result.items || [];
            this.toastService.success('Danh mục đã được thêm thành công.');
            this.visible = false;
          },
          error: (err) => {
            this.toastService.error('Có lỗi xảy ra khi thêm danh mục.');
            console.error(err);
          }
        })
      : this.categoryService.updateCategory(categoryData).pipe(
          switchMap(() => this.categoryService.getCategoriesPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedCategories = Array.isArray(result.items) ? result.items : [];
            this.toastService.success('Danh mục đã được cập nhật thành công.');
            this.visible = false;
          },
          error: (err) => {
            this.toastService.error('Có lỗi xảy ra khi cập nhật danh mục.');
            console.error(err);
          }
        });

    this.subscriptions.add(subscription);
 }


  confirmDelete(category: CategoryDto): void {
    this.confirmationService.confirm({
      message: 'Bạn có chắc chắn muốn xóa danh mục này?',
      accept: () => {
        const subscription = this.categoryService.deleteCategory(category.id).pipe(
          switchMap(() => this.categoryService.getCategoriesPagedList({ pageNumber: this.pageNumber, pageSize: this.pageSize }))
        ).subscribe({
          next: (result) => {
            this.selectedCategories = Array.isArray(result.items) ? result.items : [];
            this.toastService.success('Danh mục đã được xóa thành công.');
          },
          error: (err) => {
            this.toastService.error('Có lỗi xảy ra khi xóa danh mục.');
            console.error(err);
          }
        });

        this.subscriptions.add(subscription);
      }
    });
  }
}
