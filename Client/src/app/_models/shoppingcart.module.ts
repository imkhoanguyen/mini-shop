import { CartItem } from "./cartitem.module";
export interface ShoppingCart
{
    id:number;
    created:Date;
    updated:Date;
    cartItems:CartItem[];
}