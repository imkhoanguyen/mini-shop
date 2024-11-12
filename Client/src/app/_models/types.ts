export interface Order {
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
}

export interface OrderItem {
  id?: Number;
  productId: Number;
  quantity: Number;
  price: Number;
  orderId: Number;
  productName: string;
  productColor: string;
  productSize: string;
}

export interface Blog {
  id?: Number;
  title: string;
  content: string;
  category: string;
  userId: string;
  create: string;
  update: string;
}

export interface ShippingMethod {
  id?: Number;
  name: string;
  cost: string;
  estimatedDeliveryTime: string | null;
  create: string | null;
  update: string | null;
}
