import { Component, inject, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FooterClientComponent } from '../../../layout/footerClient/footerClient.component';
import { HeaderComponent } from '../../../layout/headerClient/header.component';
import { ProductListComponent } from '../productList/productList.component';
import { Router } from '@angular/router';
import { ProductService } from '../../../_services/product.service';
import { ProductDto } from '../../../_models/product.module';
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
    // ProductListComponent,
  ],
})
export class ProductUserComponent implements OnInit {
  productArray: any[] = [];
  productArraySmartPhone: any[] = [];
  productArrayLaptop: any[] = [];
  private productService = inject(ProductService);  
  constructor( private router: Router) {}

  ngOnInit(): void {
    this.loadProduct();
  }

loadProduct() {
    this.productService.getAllProducts().subscribe(
      (products : ProductDto[]) => {
        this.productArray = products; 

        this.productArray.forEach(product => {
          if (product.variants && product.variants.length > 0) {
            const price = product.variants[0].price;
            console.log(`Price for product ${product.id}: ${price}`);
          } else {
            console.log(`No variants for product ${product.id}`);
          }
        });
  
        this.productArraySmartPhone = products.filter(product => product.categoryIds.includes(1));
         this.productArrayLaptop = products.filter(product => product.categoryIds.includes(2));
      },
     
      (error) => {
        console.error('Error loading products:', error);
      }
    );
  }
}
