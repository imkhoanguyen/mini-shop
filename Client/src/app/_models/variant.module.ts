import { Color } from "./color.module";
import { Size } from "./size.module";

export interface Variant{
  id: number;
  price: number;
  priceSell: number;
  quantity: number;
  sizeId: Size[];
  colorId: Color[];
}
