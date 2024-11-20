import { Component, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FooterClientComponent } from '../../../layout/footerClient/footerClient.component';
import { HeaderComponent } from '../../../layout/headerClient/header.component';
import { ProductListComponent } from '../productList/productList.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-product',
  standalone: true,
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
  imports: [
    CommonModule,
    RouterModule,
    HeaderComponent,
    FooterClientComponent,
    ProductListComponent,
  ],
})
export class ProductUserComponent implements OnInit {
  productArray: any[] = [];
  productArraySmartPhone: any[] = [];
  productArrayLaptop: any[] = [];

  constructor(private productSrv: productUserService, private router: Router) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  getAllProductByCategory(categoryId: number) {
    this.productSrv
      .getAllProductByCategory(categoryId)
      .subscribe((Res: any) => {
        this.productArray = Res.data;
      });
  }
  productList() {
    console.log("Product Array Smartphone before navigating:", this.productArraySmartPhone);
  
    this.router.navigate(['/product/productList'], {
      state: { productArray: this.productArraySmartPhone },
    }).then(success => {
      console.log('Navigation success:', success);
    }).catch(err => {
      console.error('Navigation error:', err);
    });
  }

  loadProduct() {
   
   this.productArray = this.productSrv.getAllProduct();
   
  }
}
