import { Color } from "./color.module";
import { Size } from "./size.module";

export enum VariantStatus {
  Draft = 0,
  Public = 1,
}

export interface VariantBase {
  productId: number;
  price: number;
  priceSell: number;
  quantity: number;

  status: VariantStatus;
}

export interface VariantAdd extends VariantBase {
  imageFiles: File[];
  sizeId: number;
  colorId: number;
}

export interface VariantUpdate extends VariantBase {
  id: number;
  sizeId: number;
  colorId: number;
}

export interface VariantDto extends VariantBase {
  id: number;
  images: ImageVariantDto[];
  size: Size;
  color: Color;
}
export interface ImageVariantDto {
  id: number;
  imgUrl: string;
}
