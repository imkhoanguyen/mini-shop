import { nanoid } from 'nanoid';

export type CartType = {
  id: string;
  items: CartItem[];
};

export type CartItem = {
  productName: string;
  sizeName: string;
  colorName: string;
  productImage: string;
  productId: number;
  quantity: number;
  price: number;
  variantId: number;
};

export class Cart implements CartType {
  id = nanoid();
  items: CartItem[] = [];
}
