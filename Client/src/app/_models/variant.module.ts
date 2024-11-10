export interface VariantStatus {
  draft: 0;
  publish: 1;
}
export interface VariantBase {
  productId: number;
  price: number;
  priceSell: number;
  quantity: number;
  sizeId: number;
  colorId: number;
}

export interface VariantAdd extends VariantBase {
  imageFiles: File[];
}

export interface VariantUpdate extends VariantBase {
  id: number;
  imageFiles: File[];
}

export interface VariantDto extends VariantBase {
  id: number;
  images: ImageVariantDto[];
}
export interface ImageVariantDto {
  id: number;
  imgUrl: string;
}
