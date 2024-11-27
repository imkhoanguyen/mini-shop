import { OrderItem } from "./types";
export interface Orders {
    id?: Number;
    subTotal: Number;
    orderDate: string;
    address: string;
    phone: string;
    shippingFee: string;
    shippingMethodId: Number;
    create: string;
    update: string;
    userId: string;
    discountId: Number;
    discountPrice: Number;
    orderItems:OrderItem[];
  }
  
  