import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ProductUserComponent } from './component/client/product/product.component'; // Đường dẫn đến component của bạn
import { HttpClientModule } from '@angular/common/http'; // Nhập HttpClientModule
import { CommonModule } from '@angular/common';
@NgModule({
  declarations: [
  
     // Đảm bảo đã khai báo component ở đây
  ],
  imports: [
    BrowserModule, // Chỉ cần BrowserModule ở đây
    AppComponent,
    CommonModule
  ],
  providers: [],
 
})
export class AppModule { }
