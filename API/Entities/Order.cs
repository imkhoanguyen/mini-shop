using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
 {
    public class Order : BaseEntity
    {
        public decimal Total_price{get;set;}
        public DateTime Order_date{get;set;}
        public int Status {get;set;}
        public string Address{get;set;}
        public string Phone{get;set;}
        public decimal Shipping_fee{get;set;}
        public string Reciever_name{get;set;}
        public DateTime Created { get; set; }=DateTime.UtcNow;
        public DateTime Updated { get; set; }
        public int ShippingMethodId{get;set;}
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod{get;set;}
        public List<OrderItems> OrderItems{get;set;}=new List<OrderItems>();
    }
 }