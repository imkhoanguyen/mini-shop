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

export interface ProductAdd extends ProductBase {}

export interface ProductUpdate extends ProductBase {
  id: number;
}

export interface ProductDto extends ProductBase {
  id: number;
  created: Date;
  updated: Date;
  variants: VariantDto[];
}
