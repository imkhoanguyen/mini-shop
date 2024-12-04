import { Component, OnInit } from '@angular/core';
import { UserProductService } from '../../_services/user_product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-user-liked',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './product-user-liked.component.html',
  styleUrl: './product-user-liked.component.css'
})
export class ProductUserLikedComponent implements OnInit{
  productLiked: any []=[];
  userId:string="";
  constructor(
    private userProductService: UserProductService,
  ) {}
  ngOnInit(): void {
    const value = localStorage.getItem('user');
    if (value){
      const user = JSON.parse(value);
      this.userId=user.id
    }
    this.loadUserProductLiked(this.userId)
    
  }

  loadUserProductLiked(userId:string){
    this.userProductService.getFavoriteProducts(userId).subscribe(
      (response: any) => {
        this.productLiked=response
        console.log(response)
      },
      (error) => {
        console.log('error load routes', error);
      }
    );
  }

  DeleteProductLiked(productId:number){
    if (confirm("Bạn có chắc chắn muốn xóa sản phẩm này khỏi danh sách yêu thích?")) {
      this.userProductService.removeLikedProduct(this.userId, productId).subscribe(
        () => {
          // Xóa sản phẩm khỏi danh sách hiển thị
          this.productLiked = this.productLiked.filter(product => product.id !== productId);
          console.log(`Sản phẩm với ID ${productId} đã được xóa khỏi danh sách yêu thích.`);
        },
        (error) => {
          console.error("Lỗi khi xóa sản phẩm:", error);
        }
      );
    }
  }

}
