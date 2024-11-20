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
        if (this.productId) {
          this.getAllProductByCategory(+this.productId); 
        }
      });
  }
  getAllProductByCategory(categoryId: number) {
    this.productSrv.getAllProductByCategory(categoryId).subscribe(
     
      (products) => {
        this.productArray = products; 
      },

    );
  }
}
