import { CartItem } from './cart';
export interface Order {
  id?: number;
  subTotal: number;
  address: string;
  fullName: string;
  phone: string;
  shippingFee: string;
  shippingMethodId: number;
  shippingMethodDescription: string;
  shippingName: string;
  created: string;
  updated: string;
  userId: string;
  discountId?: number;
  discountPrice?: number;
  orderItems: OrderItem[];
  paymentMethod: string;
  description: string;
  strippeSessionId: string;
  status: string;
  paymentStatus: string;
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

export interface OrderItem {
  id: number;
  productId: number;
  quantity: number;
  price: number;
  productName: number;
  sizeName: number;
  colorName: number;
  orderId: number;
  productImage: string;
}

export class OrderParams {
  pageNumber: number = 1;
  pageSize: number = 5;
  search: string = '';
  orderBy: string = 'id_desc';
  selectedPaymentStatus: string = '';
  selectedStatus: string = '';
  userId: string = '';
  startDate: string = '';
  endDate: string = '';
}
