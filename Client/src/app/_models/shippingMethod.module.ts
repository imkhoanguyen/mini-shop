export interface ShippingMethodBase
{
    name:string;
    cost:number;
    estimatedDeliveryTime:string;
}
export interface ShippingMethodDto extends ShippingMethodBase
{
    id:number;
    created:Date;
    updated :Date;
}
export interface ShippingMethodAdd extends ShippingMethodBase
{}
export interface ShippingMethodUpdate extends ShippingMethodBase
{
    id:number;
}

export interface ShippingMethod{
    id?:number;
    name:string;
    cost:number;
    estimatedDeliveryTime:string;
    created?:Date;
}