import { Component, inject, Input, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { GalleriaModule } from 'primeng/galleria';
import { ImageModule } from 'primeng/image';
import { InputNumberModule } from 'primeng/inputnumber';
import { TooltipModule } from 'primeng/tooltip';
import { ProductService } from '../../../_services/product.service';
import { CartService } from '../../../_services/cart.service';
import { ToastrService } from '../../../_services/toastr.service';
import { ProductDto } from '../../../_models/product.module';
import { VariantDto } from '../../../_models/variant.module';
import { CartItem } from '../../../_models/cart';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { ReviewComponent } from '../../review/review.component';
import { DividerModule } from 'primeng/divider';
import { UserProductService } from '../../../_services/user_product.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [
    ButtonModule,
    GalleriaModule,
    ImageModule,
    TooltipModule,
    FormsModule,
    ReactiveFormsModule,
    InputNumberModule,
    BreadcrumbModule,
    ReviewComponent,
    DividerModule,
  ],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css',
})
export class ProductDetailComponent implements OnInit {
  @Input() productId: number = 0;
  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private toastrService = inject(ToastrService);
  private route = inject(ActivatedRoute);
  private userProductService = inject(UserProductService);
  productDetail!: ProductDto;
  listImage: string[] = [];

  selectedVariant!: VariantDto;
  count = 1;

  // breadcrumb
  items: MenuItem[] | undefined;
  userId:string="";

  ngOnInit(): void {
    // breadcrumb items
    const value = localStorage.getItem('user');
    if (value){
      const user = JSON.parse(value);
      this.userId=user.id
    }
    this.productId = +this.route.snapshot.paramMap.get('productId')!;
    this.loadProduct();
  }

  // img view
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

        this.items = [
          { label: 'Trang chủ', routerLink: '/home' },
          { label: 'Sản phẩm', routerLink: '/shop' },
          {
            label: this.productDetail.name,
            routerLink: `/shop/${this.productId}`,
          },
        ];
      },
      error: (er) => console.log(er),
    });
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
  LikeProduct(): void {
    if (!this.userId) {
      console.error("Người dùng chưa đăng nhập.");
      return;
    }
    console.log(this.userId)
    console.log("Thêm sản phẩm yêu thích:", this.productId);
    this.userProductService.addLikedProduct(this.userId, this.productId).subscribe(
      (response: any) => {
        console.log("Thêm sản phẩm yêu thích thành công:", response);
        alert("Sản phẩm đã được thêm vào danh sách yêu thích!");
      },
      (error) => {
        console.error("Lỗi khi thêm sản phẩm vào danh sách yêu thích:", error);
        alert("Không thể thêm sản phẩm vào danh sách yêu thích.");
      }
    );
  }
}
