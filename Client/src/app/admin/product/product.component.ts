import { ProductService } from '../services/product.service';
import { SharedModule } from '../../shared/shared.module';
import { Component } from '@angular/core';
import { Product } from '../models/product.module';
import { TableRowCollapseEvent, TableRowExpandEvent } from 'primeng/table';


@Component({
  selector: 'app-product',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  products!: Product[];

    expandedRows = {};

    constructor(private productService: ProductService) {}

    ngOnInit() {
        this.productService.getProductAllPaging().subscribe(
            (data: Product[]) => {
                this.products = data;
            }
        );
    }

    expandAll() {
        //this.expandedRows = this.products.reduce((acc, p) => (acc[p.id] = true) && acc, {});
    }

    collapseAll() {
        this.expandedRows = {};
    }

    // getSeverity(status: string) {
    //     switch (status) {
    //         case 'INSTOCK':
    //             return 'success';
    //         case 'LOWSTOCK':
    //             return 'warning';
    //         case 'OUTOFSTOCK':
    //             return 'danger';
    //     }
    // }

    // getStatusSeverity(status: string) {
    //     switch (status) {
    //         case 'PENDING':
    //             return 'warning';
    //         case 'DELIVERED':
    //             return 'success';
    //         case 'CANCELLED':
    //             return 'danger';
    //     }
    // }

    onRowExpand(event: TableRowExpandEvent) {
        //this.messageService.add({ severity: 'info', summary: 'Product Expanded', detail: event.data.name, life: 3000 });
    }

    onRowCollapse(event: TableRowCollapseEvent) {
        //this.messageService.add({ severity: 'success', summary: 'Product Collapsed', detail: event.data.name, life: 3000 });
    }
}
