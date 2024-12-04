import { Component, OnInit } from '@angular/core';
import { UserProductService } from '../../_services/user_product.service';
import { CartItem } from '../../_models/cart';
import { CartService } from '../../_services/cart.service';
import { ToastrService } from '../../_services/toastr.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-user-liked',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './product-user-liked.component.html',
  styleUrls: ['./product-user-liked.component.css']
})
export class ProductUserLikedComponent implements OnInit {
  productLiked: any[] = [];
  userId: string = '';
  productSizeColor: any[] = [];
  selectedSizeColor: any = {};  // Store selected sizeColor for each product

  constructor(
    private userProductService: UserProductService,
    private cartService: CartService,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
    const value = localStorage.getItem('user');
    if (value) {
      const user = JSON.parse(value);
      this.userId = user.id;
    }
    this.loadUserProductLiked(this.userId);
  }

  loadUserProductLiked(userId: string) {
    this.userProductService.getFavoriteProducts(userId).subscribe(
      (response: any) => {
        this.productLiked = response;
        console.log(this.productLiked);
        this.productSizeColor = [];

        for (const product of this.productLiked) {
          if (product.variants && product.variants.length > 0) {
            for (const variant of product.variants) {
              this.productSizeColor.push([
                product.id,
                `${variant.size.name} + ${variant.color.name}`,
                variant.id,
                variant.price
              ]);
            }
          }
        }
        console.log(this.productSizeColor);
      },
      (error) => {
        console.log('Lỗi khi tải danh sách sản phẩm yêu thích:', error);
      }
    );
  }

  DeleteProductLiked(productId: number) {
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

  addToCart(product: any) {
    const selectedValue = this.selectedSizeColor[product.id]?.selectedValue;
    const variantId = this.selectedSizeColor[product.id]?.variantId;
    const price = this.selectedSizeColor[product.id]?.price;

    console.log(variantId);
    console.log(price);

    // Kiểm tra xem đã chọn size và color chưa
    if (!selectedValue || selectedValue === 'Chọn size và color') {
      this.toastrService.error('Vui lòng chọn size và color trước khi thêm vào giỏ hàng.');
      return;
    }

    // Tách size và color từ selected value
    const { size, color } = this.splitSizeColor(selectedValue);

    // Thêm sản phẩm vào giỏ hàng
    const cardItem: CartItem = {
      productId: product.id,
      colorName: color || "",
      sizeName: size || "",
      price: price || 0,
      productImage: product.image?.imgUrl || "",
      quantity: 1,
      productName: product.name || "",
      variantId: variantId || 0,
    };

    this.cartService.addItemToCart(cardItem, cardItem.quantity).then((success) => {
      if (success) {
        this.toastrService.success('Thêm sản phẩm vào giỏ hàng thành công!');
      } else {
        this.toastrService.error('Thêm sản phẩm vào giỏ hàng thất bại.');
      }
    });
  }

  getSizeColorsForProduct(productId: number): string[] {
    // Lọc productSizeColor để lấy các size-color phù hợp với productId
    return this.productSizeColor
      .filter(sizeColor => sizeColor[0] === productId)
      .map(sizeColor => sizeColor[1]);
  }

  splitSizeColor(sizeColor: string): { size: string, color: string } {
    const [size, color] = sizeColor.split(' + ');
    return { size, color };
  }

  onSizeColorChange(event: any, productId: number): void {
    const selectedValue = event.target.value;
    
    // Lưu lại giá trị sizeColor đã chọn cho sản phẩm
    this.selectedSizeColor[productId] = {
      selectedValue: selectedValue,
      variantId: 0,
      price: 0
    };

    // Lấy variantId tương ứng
    const selectedVariant = this.productSizeColor.find(sizeColor =>
      sizeColor[0] === productId && sizeColor[1] === selectedValue
    );

    if (selectedVariant) {
      // Cập nhật variantId và price cho sản phẩm trong selectedSizeColor
      this.selectedSizeColor[productId].variantId = selectedVariant[2];
      this.selectedSizeColor[productId].price = selectedVariant[3];
    }
  }
}
