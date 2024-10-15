import { Variant } from "./variant.module";
import { Category } from "./category.module";

export interface Product
{
  id: number;
  name: string;
  description: string;
  created: Date;
  updated: Date;
  variants: Variant[];
  categoryIds: Category[];
  status: number;
}

export interface ProductAdd{
  name: string;
  description: string;
  categoryIds: Category[];
}
export interface ProductUpdate{
  id: number;
  name: string;
  description: string;
  categoryIds: Category[];
  status: number;
}
