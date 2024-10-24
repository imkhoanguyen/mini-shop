import { Component, NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { SplitButtonModule } from 'primeng/splitbutton';
import { InputTextModule } from 'primeng/inputtext';
import { ProductService } from '../../../_services/product.service';
import { FormsModule,FormBuilder } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
   InputTextModule,
   ButtonModule,
   SplitButtonModule,
   FormsModule,
   CalendarModule,
   TableModule
  ],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  startDate: Date | null = null;
  endDate: Date | null = null;
  mask: string = '99/99/9999';
expanded: any;
orders = [
  {
    id: 1,
    fullname: 'Luan',
    address: '127/18 p7,q5',
    quantity: 7,
    price: 1234000,
    created: '10/10/2020',
    status: 'Not Accept'
  }
];
  
  onSearch() {
    console.log('Ngày bắt đầu:', this.startDate);
    console.log('Ngày kết thúc:', this.endDate);
  }
  // loadProduct(){
  //   const productSub=this.productService
  //     .getProductAllPaging(this.pageNumber, this.pageSize,this.searchString)
  // }
  // pageSize(pageNumber: (pageNumber: any, pageSize: any, searchString: string) => void, pageSize: any, searchString: string) {
  //   throw new Error('Method not implemented.');
  // }
  // pageNumber(pageNumber: any, pageSize: any, searchString: string) {
  //   throw new Error('Method not implemented.');
  
}
