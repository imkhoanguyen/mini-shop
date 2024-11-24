import { Component, OnInit } from '@angular/core';
import { productUserService } from '../../../_services/productUser.service';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from '../../../layout/headerClient/header.component';
import { FooterClientComponent } from '../../../layout/footerClient/footerClient.component';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ProductGet } from '../../../_models/product.module'; // You can define a specific interface for Product if you want
import { ApiService } from '../shared/api.service';
@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './productDetail.component.html',
  styleUrls: ['./productDetail.component.css'], // Corrected styleUrls, not styleUrl
  imports: [CommonModule, HeaderComponent, FooterClientComponent],
})
export class ProductDetailComponent implements OnInit {
  productId?: string;
  productFound: any = {}; // Change to object instead of array
  productArrayRelated: ProductGet[] = [];
  showAdd: boolean = true;
  showRemove: boolean = false;
  
  constructor(
    private route: ActivatedRoute,
    private productSrv: productUserService,
    private api :ApiService 
 
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.productId = params.get('id') || '';
      console.log('Received Product ID:', this.productId);
      if (this.productId) {
        this.getProductDetail(+this.productId); // Ensure the id is passed as a number
      }
    });
  }

  getProductDetail(ProductId: number): void {
    this.productSrv.getProductDetail(ProductId).subscribe(
      (product) => {
        this.productFound = product;
        const categoryId = this.productFound.categoryIds;
        this.getAllProductRelatedByCategory(categoryId);
      },
      (error) => {
        console.error('Error loading product details:', error);
      }
    );
  }

  getAllProductRelatedByCategory(categoryId: number) {
    this.productSrv
      .getAllProductByCategory(categoryId)
      .subscribe((products: ProductGet[]) => {
        this.productArrayRelated = products;
        console.log('productRelated Array' + this.productArrayRelated);
      });
  }

  addToCart(data:ProductGet) {
    this.showAdd = false ;
    this.showRemove  = true ;
this.api.addToCart(data)
  }
  removeItem() {
    this.showAdd = true ;
    this.showRemove = false ;
  }
}
