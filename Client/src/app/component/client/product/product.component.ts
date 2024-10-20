import { Component, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-product',
  standalone: true,
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
  imports: [CommonModule] 
})
export class ProductUserComponent implements OnInit {
  productArray: any[] = [];
 
  constructor(private productSrv: productUserService) {}
  ngOnInit(): void {
    this.loadProduct();
  }

  
  loadProduct() {
    this.productSrv.getAllProduct().subscribe((Res: any) => {
      this.productArray = Res.data; // Gán dữ liệu vào productArray
      console.log('Products loaded:', this.productArray); // In ra giá trị của productArray
    }, (error) => {
      console.error('Error fetching products:', error); // In lỗi nếu có
    });
  }
}
