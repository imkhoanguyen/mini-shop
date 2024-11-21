import { VariantDto } from "./variant.module";

export enum ProductStatus {
  Draft = 0,
  Publish = 1,
}

export interface ProductBase {
  name: string;
  description: string;
  status: ProductStatus;
  categoryIds: number[];
}

export interface ProductAdd extends ProductBase {
  imageFile?: File;
}

export interface ProductUpdate extends ProductBase {
  id: number;
  imageFile?: File;
}

export interface ProductDto extends ProductBase {
  id: number;
  created: Date;
  updated: Date;
  image: ImageProductDto;
  variants: VariantDto[];
}
export interface ImageProductDto {
  id: number;
  imgUrl: string;
}
export interface ProductGet extends ProductBase
{
  
}
