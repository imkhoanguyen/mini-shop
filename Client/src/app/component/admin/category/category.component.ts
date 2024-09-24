import { Component, OnInit } from '@angular/core';
import { Category } from '../../../_models/category.module';
import { CategoryService } from '../../../_services/category.service';
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
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';

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
    ConfirmDialogModule
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  selectedCategories: Category[] = [];
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Danh Mục';
  categoryForm: any;

  paginatedCategory: any;
  totalRecords: number = 0;
  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];

  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = "";

  constructor(
    private builder: FormBuilder,
    public categoryService: CategoryService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadCategories();
  }
  private showMessage(severity: string, summary: string, detail: string, life: number = 3000) {
    this.messageService.add({ severity, summary, detail, life });
  }
  initializeForm() {
    this.categoryForm = this.builder.group({
      id: this.builder.control(0),
      name: this.builder.control('', Validators.required),
    });
  }
  onPageChange(event: any): void {
    this.pageNumber = event.page + 1;
    this.loadCategories();
  }
  onPageSizeChange(newPageSize: any): void {
    this.pageSize = newPageSize.value;
    this.pageNumber = 1;
    this.loadCategories();
  }

  onSearch(): void {
    this.pageNumber = 1;
    this.loadCategories();
  }
  loadCategories(): void {
    this.categoryService.getCategoriesAllPaging(this.pageNumber, this.pageSize, this.searchString).subscribe(
        (pagination) => {
          console.log(pagination);
          this.paginatedCategory = pagination;
          this.totalRecords = pagination.count;
          console.log(this.totalRecords);
        },
        (error) => {
          const errorMessage = error.error?.message || 'L��i tải danh mục';
          this.showMessage('error', 'Thất Bại', errorMessage);
        }
    );
  }

  showDialog() {
    this.visible = true;
    this.categoryForm.reset();
    this.btnText = 'Thêm';
    this.headerText = 'Thêm Danh Mục';
  }
  openUpdateDialog(category: Category) {
    this.categoryForm.setValue({
      id: category.id,
      name: category.name,
    });
    this.btnText = 'Cập Nhật';
    this.headerText = 'Cập Nhật Danh Mục';
    this.visible = true;
  }
  confirmDelete(category: Category, event: Event) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bạn muốn xóa Danh mục này?',
      header: 'Xác nhận Xóa',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: "p-button-danger p-button-text",
      rejectButtonStyleClass: "p-button-text p-button-text",
      acceptIcon: "none",
      rejectIcon: "none",

      accept: () => {
        this.categoryService.deleteCategory(category).subscribe(
          (res) => {
            this.showMessage('success', 'Thành Công', 'Xóa danh mục thành công');
            this.loadCategories();
          },
          (error) => {
            const errorMessage = error.error?.message || 'Xóa danh mục thất bại';
            this.showMessage('error', 'Thất Bại', errorMessage);
          }
        );
      },

    });
  }


  onSubmit() {
  const data: Category = this.categoryForm.value;
  console.log(data);
  if (data.id == null) {
    this.categoryService.addCategory(data).subscribe(
      (res) => {
        this.showMessage('success', 'Thành Công', 'Thêm danh mục thành công');
        this.loadCategories();
        setTimeout(() => {
          this.visible = false;
        }, 1000);
      },
      (error) => {
        const errorMessage = error.error?.message || 'Thêm danh mục thất bại';
        this.showMessage('error', 'Thất Bại', errorMessage);
      }
    );
  } else {
    this.categoryService.updateCategory(data).subscribe(
      (res) => {
        this.showMessage('success', 'Thành Công', 'Cập nhật danh mục thành công');
        this.loadCategories();
        setTimeout(() => {
          this.visible = false;
        }, 1000);
      },
      (error) => {
        const errorMessage = error.error?.message || 'Cập nhật danh mục thất bại';
        this.showMessage('error', 'Thất Bại', errorMessage);
      }
    );
  }
}


  onSelectCategory(event: any) {
    this.selectedCategories = event.value;
  }

}
