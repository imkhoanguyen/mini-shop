import { Component, inject, Input, OnInit } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { ProductService } from '../../../_services/product.service';
import { ProductDto } from '../../../_models/product.module';
import { GalleriaModule } from 'primeng/galleria';
import { ImageModule } from 'primeng/image';
import { TooltipModule } from 'primeng/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';
import { VariantDto } from '../../../_models/variant.module';
import { CartItem } from '../../../_models/cart';
import { CartService } from '../../../_services/cart.service';
import { ToastrService } from '../../../_services/toastr.service';

@Component({
  selector: 'app-quickview-product',
  standalone: true,
  imports: [
    DialogModule,
    ButtonModule,
    GalleriaModule,
    ImageModule,
    TooltipModule,
    FormsModule,
    ReactiveFormsModule,
    InputNumberModule,
  ],
  templateUrl: './quickview-product.component.html',
  styleUrl: './quickview-product.component.css',
})
export class QuickviewProductComponent {
  @Input() productId: number = 0;
  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private toastrService = inject(ToastrService);
  visible: boolean = false;
  productDetail!: ProductDto;
  listImage: string[] = [];

  selectedVariant!: VariantDto;
  count = 1;

  responsiveOptions = [
    {
      breakpoint: '1024px',
      numVisible: 5,
    },
    {
      breakpoint: '768px',
      numVisible: 3,
    },
    {
      breakpoint: '560px',
      numVisible: 1,
    },
  ];

  loadProduct() {
    this.productService.getProductById(this.productId).subscribe({
      next: (response) => {
        this.productDetail = response;
        this.listImage = this.productDetail.variants.flatMap((v) =>
          v.images.map((i) => i.imgUrl)
        );
        this.listImage.unshift(this.productDetail.image.imgUrl);
        this.selectedVariant = response.variants[0];

        this.showDialog();
      },
      error: (er) => console.log(er),
    });
  }

  showDialog() {
    this.visible = true;
  }

  closeDialog() {
    this.visible = false;
    this.count = 1;
    this.listImage = [];
  }

  onVariantSelect(variant: VariantDto): void {
    this.selectedVariant = variant;
    console.log('Selected Variant:', this.selectedVariant);
  }

  addToCart() {
    const cardItem: CartItem = {
      productId: this.productId,
      colorName: this.selectedVariant.color.name,
      sizeName: this.selectedVariant.size.name,
      price: this.selectedVariant.priceSell,
      productImage: this.productDetail.image.imgUrl,
      quantity: this.count,
      productName: this.productDetail.name,
      variantId: this.selectedVariant.id,
    };
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
