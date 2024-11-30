import { CartItem } from './cart';
import { OrderItem } from './types';
export interface Orders {
  id?: Number;
  subTotal: Number;
  address: string;
  fullName: string;
  phone: string;
  shippingFee: string;
  shippingMethodId: Number;
  create: string;
  update: string;
  userId: string;
  discountId: Number;
  discountPrice: Number;
  orderItems: OrderItem[];
}

export interface OrderAdd {
  // address & user information
  address: string;
  phone: string;
  fullName: string;
  // shipping method information
  shippingFee: number;
  shippingMethodId: number;
  // discount information
  discountId?: number;
  discountPrice?: number;
  // order information
  description: string;
  subTotal: number;
  // list item
  items: CartItem[];
  paymentMethod: string;
}
