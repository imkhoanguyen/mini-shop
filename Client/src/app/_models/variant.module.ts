import { Image} from "./image.module";
export interface Variant{
  id: number;
  price: number;
  priceSell: number;
  quantity: number;
  sizeId: number;
  colorId: number;
  imageUrls: Image[];
}
export interface VariantAdd{
  price: number;
  priceSell: number;
  quantity: number;
  sizeId: number;
  colorId: number;
  productId: number;
}

