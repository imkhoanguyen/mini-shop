import { Component, NgModule } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TerminalModule } from 'primeng/terminal';
import { TableModule, TableRowCollapseEvent, TableRowExpandEvent } from 'primeng/table';
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
@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [TabViewModule,
    TagModule,
    TerminalModule,
    TableModule,
    ButtonModule,
    RatingModule,
    ToastModule,
    CommonModule,
    FormsModule,
    DropdownModule,
    InputSwitchModule],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.css',
  providers: [MessageService]
})
export class ProductlistComponent {
  products!: Product[];
  formGroup: FormGroup | undefined;

    expandedRows: { [key: number]: boolean } = {};

    constructor(private productService: ProductService,
      private messageService: MessageService,
      private router: Router) {}

    ngOnInit() {
        this.productService.getAllProduct().subscribe(
            (products: Product[]) => {
                this.products = products;
            },
            (error) => {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load products', life: 3000 });
            }
        );
        
    }

    expandAll() {
        this.expandedRows = this.products.reduce((acc: { [key: number]: boolean }, p: Product) => {
            acc[p.id] = true;
            return acc;
        }, {});
    }

    collapseAll() {
        this.expandedRows = {};
    }

    getSeverity(status: string): 'success' | 'secondary' | 'info' | 'warning' | 'danger' | 'contrast' | undefined  {
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

    getStatusSeverity(status: string): 'success' | 'secondary' | 'info' | 'warning' | 'danger' | 'contrast' | undefined {
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
        this.messageService.add({ severity: 'info', summary: 'Product Expanded', detail: event.data.name, life: 3000 });
    }

    onRowCollapse(event: TableRowCollapseEvent) {
        this.messageService.add({ severity: 'success', summary: 'Product Collapsed', detail: event.data.name, life: 3000 });
    }
    addProduct(){
      this.router.navigateByUrl('/admin/product/productadd');
    }
}
