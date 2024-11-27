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
  visible: boolean = false;
  productDetail!: ProductDto;
  listImage: string[] = [];

  selectedVariant = 0;
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
    this.selectedVariant = 0;
    this.count = 1;
    this.listImage = [];
  }

  onVariantSelect(variantId: number): void {
    this.selectedVariant = variantId;
    console.log('Selected Variant ID:', this.selectedVariant);
  }
}
