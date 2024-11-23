export interface DiscountBase
{
    name:string;
    amountOff:number;
    percentOff:number;
    promotionCode:string;
}
export interface  DiscountDto extends  DiscountBase
{
    id:number;
}
export interface  DiscountAdd extends  DiscountBase
{}
export interface DiscountUpdate extends  DiscountBase
{
    id:number;
}