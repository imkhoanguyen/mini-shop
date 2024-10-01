import { Component, OnInit, OnDestroy } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { CommonModule } from '@angular/common';
import {TableModule,TableRowCollapseEvent,TableRowExpandEvent} from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import {FormBuilder,ReactiveFormsModule,Validators,} from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';
import { Subscription } from 'rxjs';
import { Product } from '../../../_models/product.module';
import { Image } from '../../../_models/image.module';
import { ProductService } from '../../../_services/product.service';
import { Router} from '@angular/router';

@Component({
  selector: 'app-product',
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
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent {
  products!: Product[];
  productForm: any;


  expandedRows: { [key: number]: boolean } = {};
  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];
  first: number = 0;
  paginatedProduct: any;
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = '';

  private subscriptions: Subscription = new Subscription();
  uploadedFiles!: Image[];


  constructor(
    private builder: FormBuilder,
    private productService: ProductService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.loadProducts();
  }
  initializeForm(): void {
    this.productForm = this.builder.group({
      id: [0],
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }
  expandAll() {
    this.expandedRows = this.products.reduce(
      (acc: { [key: number]: boolean }, p: Product) => {
        acc[p.id] = true;
        return acc;
      },
      {}
    );
  }
  collapseAll() {
    this.expandedRows = {};
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  private showMessage(
    severity: string,
    summary: string,
    detail: string,
    life: number = 3000
  ): void {
    this.messageService.add({ severity, summary, detail, life });
  }

  private handleError(error: any, action: string): void {
    const errorMessage = error.error?.message || `${action} thất bại`;
    this.showMessage('error', 'Thất Bại', errorMessage);
  }
  onSearch(): void {
    this.pageNumber = 1;
    this.loadProducts();
  }
  onPageChange(event: any): void {
    this.first = event.first;
    this.pageNumber = event.page + 1;
    this.pageSize = event.rows;
    this.loadProducts();
  }

  onPageSizeChange(newPageSize: any): void {
    this.pageSize = newPageSize.value;
    this.pageNumber = 1;
    this.loadProducts();
  }

  loadProducts() {
    const productSub = this.productService
      .getProductAllPaging(this.pageNumber, this.pageSize, this.searchString)
      .subscribe(
        (pagination) => {
          this.paginatedProduct = pagination.items;
          this.totalRecords = pagination.totalCount;
        },
        (error) => this.handleError(error, 'Lấy sản phẩm')
      );
    this.subscriptions.add(productSub);
  }
  getSeverity(
    status: string
  ):
    | 'success'
    | 'secondary'
    | 'info'
    | 'warning'
    | 'danger'
    | 'contrast'
    | undefined {
    switch (status) {
      case 'INSTOCK':
        return 'success';
      case 'LOWSTOCK':
        return 'warning';
      case 'OUTOFSTOCK':
        return 'danger';
      default:
        return 'secondary';
    }
  }
  getStatusSeverity(
    status: string
  ):
    | 'success'
    | 'secondary'
    | 'info'
    | 'warning'
    | 'danger'
    | 'contrast'
    | undefined {
    switch (status) {
      case 'success':
        return 'success';
      case 'warning':
        return 'warning';
      case 'danger':
        return 'danger';
      case 'unknown':
        return undefined;
      default:
        return 'secondary';
    }
  }
  onRowExpand(event: TableRowExpandEvent) {
    this.messageService.add({
      severity: 'info',
      summary: 'Product Expanded',
      detail: event.data.name,
      life: 3000,
    });
  }
  onRowCollapse(event: TableRowCollapseEvent) {
    this.messageService.add({
      severity: 'success',
      summary: 'Product Collapsed',
      detail: event.data.name,
      life: 3000,
    });
  }
  addProduct() {
    this.router.navigateByUrl('/admin/product/productadd');
  }
  editProduct(id: number) {
    this.productService.getProductById(id).subscribe(() => {
      this.router.navigateByUrl('/admin/product/productadd/' + id);
    });
  }

  deleteProduct(id: number) {

  }

}
