import { Component, NgModule } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import {
  TableModule,
  TableRowCollapseEvent,
  TableRowExpandEvent,
} from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { ToastModule } from 'primeng/toast';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Product } from '../../../../_models/product.module';
import { ProductService } from '../../../../_services/product.service';
import { DropdownModule } from 'primeng/dropdown';
import { Route, Router } from '@angular/router';
import { InputSwitchModule } from 'primeng/inputswitch';
import { Subscription } from 'rxjs';
import { PaginatorModule } from 'primeng/paginator';
@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [
    TabViewModule,
    TagModule,
    TerminalModule,
    TableModule,
    ButtonModule,
    RatingModule,
    ToastModule,
    CommonModule,
    FormsModule,
    DropdownModule,
    InputSwitchModule,PaginatorModule
  ],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.css',
  providers: [MessageService],
})
export class ProductlistComponent {
  products!: Product[];
  formGroup: FormGroup | undefined;

  expandedRows: { [key: number]: boolean } = {};
  pageSizeOptions = [
    { label: '5', value: 5 },
    { label: '10', value: 10 },
    { label: '20', value: 20 },
    { label: '50', value: 50 },
  ];
  paginatedProduct: any;
  totalRecords: number = 0;
  pageSize: number = 5;
  pageNumber: number = 1;
  searchString: string = '';

  private subscriptions: Subscription = new Subscription();

  constructor(
    private productService: ProductService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();
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

  private showMessage(severity: string, summary: string, detail: string, life: number = 3000): void {
    this.messageService.add({ severity, summary, detail, life });
  }

  private handleError(error: any, action: string): void {
    const errorMessage = error.error?.message || `${action} thất bại`;
    this.showMessage('error', 'Thất Bại', errorMessage);
  }
  onPageChange(event: any): void {
    this.pageNumber = event.page + 1;
    this.loadProducts();
  }
  onSearch(): void {
    this.pageNumber = 1;
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
          this.paginatedProduct = pagination;
          this.totalRecords = pagination.count;
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
    this.router.navigateByUrl('/admin/product/add' + id);
  }
  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(
      (res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Delete product success',
          life: 3000,
        });
        this.products = this.products.filter((p) => p.id !== id);
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to delete product',
          life: 3000,
        });
      }
    );
  }
}
