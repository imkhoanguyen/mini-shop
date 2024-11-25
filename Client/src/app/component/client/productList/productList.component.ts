import { Component, inject, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from '../../../layout/headerClient/header.component';
import { FooterClientComponent } from '../../../layout/footerClient/footerClient.component';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ProductDetailComponent } from '../productDetail/productDetail.components';
import { ApiService } from '../shared/api.service';
import { ProductDto, ProductGet } from '../../../_models/product.module';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './productList.component.html',
  // templateUrl : './productL'
  styleUrl: './productList.component.css',
  imports: [CommonModule, HeaderComponent, FooterClientComponent ,  RouterModule ,ProductDetailComponent],
})
export class ProductListComponent implements OnInit {
    productId?: string; // Ma
    productArray: any[] = [];
    showAdd: boolean = true;
    showRemove: boolean = false;
    private productService = inject(ProductService)
    constructor(private route: ActivatedRoute,private api :ApiService) {}
    ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
        this.productId = params.get('id') || '';
        console.log('Received Product ID:', this.productId);
        if (this.productId) {
          this.getAllProductByCategory(+this.productId);
        }
      });
  }
  getAllProductByCategory(categoryId: number) {
    this.productService.getAllProductByCategory(categoryId).subscribe(
      (products : ProductDto[]) => {
        this.productArray = products;
        console.log ("----", this.productArray)
      },

    );
  }



  addToCart(data: any) {
    this.showAdd = false;
    this.showRemove = true;
    this.api.addToCart(data);
  }
  removeItem(data: ProductGet) {
    this.showAdd = true;
    this.showRemove = false;
    this.api.removeToCart(data)
  }
}
