import { Component, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from '../../../layout/headerClient/header.component';
import { FooterClientComponent } from '../../../layout/footerClient/footerClient.component';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './productList.component.html',
  styleUrl: './productList.component.css',
  imports: [CommonModule, HeaderComponent, FooterClientComponent],
})
export class ProductListComponent implements OnInit {
    productId?: string; // Ma
    productArray: any[] = [];
    constructor(private route: ActivatedRoute, private productSrv: productUserService) {}


  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
        this.productId = params.get('id') || '';
        console.log('Received Product ID:', this.productId);
        // Gọi hàm lấy sản phẩm theo ID
        if (this.productId) {
          this.getAllProductByCategory(+this.productId); // Chuyển đổi sang số
        }
      });

  }


  
  getAllProductByCategory(categoryId: number) {
    this.productSrv.getAllProductByCategory(categoryId).subscribe((Res: any) => {
      this.productArray = Res.data;
      console.log('Products:', this.productArray); // Kiểm tra dữ liệu sản phẩm
    });
  }
}