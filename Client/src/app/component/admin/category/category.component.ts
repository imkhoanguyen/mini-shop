import { Component, OnInit, OnDestroy } from '@angular/core';
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
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';
import { Subscription } from 'rxjs';

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
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit, OnDestroy {
  selectedCategories: Category[] = [];
  visible: boolean = false;
  btnText: string = 'Thêm';
  headerText: string = 'Thêm Danh Mục';
  categoryForm: any;
  
  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];
  paginatedCategory: any;
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = "";

  private subscriptions: Subscription = new Subscription();

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

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private showMessage(severity: string, summary: string, detail: string, life: number = 3000): void {
    this.messageService.add({ severity, summary, detail, life });
  }

  private handleError(error: any, action: string): void {
    const errorMessage = error.error?.message || `${action} thất bại`;
    this.showMessage('error', 'Thất Bại', errorMessage);
  }

  initializeForm(): void {
    this.categoryForm = this.builder.group({
      id: [0],
      name: ['', Validators.required],
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
    const categorySub = this.categoryService.getCategoriesAllPaging(this.pageNumber, this.pageSize, this.searchString)
      .subscribe(
        (pagination) => {
          this.paginatedCategory = pagination;
          this.totalRecords = pagination.count;
        },
        (error) => this.handleError(error, 'Lấy danh mục')
      );
    this.subscriptions.add(categorySub);
  }

  showDialog(): void {
    this.visible = true;
    this.categoryForm.reset();
    this.btnText = 'Thêm';
    this.headerText = 'Thêm Danh Mục';
  }

  openUpdateDialog(category: Category): void {
    this.categoryForm.setValue({
      id: category.id,
      name: category.name,
    });
    this.btnText = 'Cập Nhật';
    this.headerText = 'Cập Nhật Danh Mục';
    this.visible = true;
  }

  confirmDelete(category: Category, event: Event): void {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bạn muốn xóa Danh mục này?',
      header: 'Xác nhận Xóa',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: "p-button-danger p-button-text",
      rejectButtonStyleClass: "p-button-text",
      acceptIcon: "none",
      rejectIcon: "none",
      accept: () => this.deleteCategory(category),
    });
  }

  private deleteCategory(category: Category): void {
    const deleteSub = this.categoryService.deleteCategory(category).subscribe(
      (res) => {
        this.showMessage('success', 'Thành Công', 'Xóa danh mục thành công');
        this.loadCategories();
      },
      (error) => this.handleError(error, 'Xóa danh mục')
    );
    this.subscriptions.add(deleteSub);
  }

  onSubmit(): void {
    const data: Category = this.categoryForm.value;
    if (!data.id) {
      this.addCategory(data);
    } else {
      this.updateCategory(data);
    }
  }

  public addCategory(data: Category): void {
    const addSub = this.categoryService.addCategory(data).subscribe(
      (res) => {
        this.showMessage('success', 'Thành Công', 'Thêm danh mục thành công');
        this.loadCategories();
        this.closeDialog();
      },
      (error) => this.handleError(error, 'Thêm danh mục')
    );
    this.subscriptions.add(addSub);
  }

  private updateCategory(data: Category): void {
    const updateSub = this.categoryService.updateCategory(data).subscribe(
      (res) => {
        this.showMessage('success', 'Thành Công', 'Cập nhật danh mục thành công');
        this.loadCategories();
        this.closeDialog();
      },
      (error) => this.handleError(error, 'Cập nhật danh mục')
    );
    this.subscriptions.add(updateSub);
  }

  private closeDialog(): void {
    this.visible = false;
  }

  onSelectCategory(event: any): void {
    this.selectedCategories = event.value;
  }
}
