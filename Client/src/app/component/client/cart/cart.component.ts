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
    templateUrl: './cart.component.html',
    styleUrl: './cart.component.css',
    imports: [
      CommonModule,
      RouterModule,
      HeaderComponent,
      FooterClientComponent,
      ],
  })
  export class  CartPageComponent implements OnInit
  {
    showProduct :any= [];
constructor (private api :)
  }

