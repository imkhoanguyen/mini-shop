import { Component, OnInit } from '@angular/core';
import { UserProductService } from '../../_services/user_product.service';
import { CommonModule } from '@angular/common';
import { CartItem } from '../../_models/cart';
import { CartService } from '../../_services/cart.service';
import { ToastrService } from '../../_services/toastr.service';

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
    private cartService: CartService,
    private toastrService:ToastrService
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
  addToCart(product:any){
    // console.log(product)
    const cardItem: CartItem = {
      productId: product.id, // Lấy id sản phẩm
      colorName: product.variants[0]?.color.name || "", // Lấy tên màu từ variant đầu tiên (nếu có)
      sizeName: product.variants[0]?.size.name || "", // Lấy tên kích thước từ variant đầu tiên (nếu có)
      price: product.variants[0]?.price || 0, // Lấy giá từ variant đầu tiên (nếu có)
      productImage: product.image?.imgUrl || "", // Lấy đường dẫn ảnh sản phẩm (nếu có)
      quantity: 1, // Mặc định số lượng là 1
      productName: product.name || "", // Lấy tên sản phẩm
      variantId: product.variants[0]?.id || 0,
    };
    console.log(cardItem)
    this.cartService
      .addItemToCart(cardItem, cardItem.quantity)
      .then((success) => {
        if (success) {
          this.toastrService.success('Thêm sản phẩm vào giỏ hành thành công!');
        } else {
          this.toastrService.error('Thêm sản phẩm vào giở hàng thất bại.');
        }
      });
  }

}
