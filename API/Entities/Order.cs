using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
 {
    public class Order : BaseEntity
    {
        public decimal total_price{get;set;}
        public DateTime order_date{get;set;}
        public int Status {get;set;}
        public string address{get;set;}
        public string phone{get;set;}
        public decimal shipping_fee{get;set;}
        public string reciever_name{get;set;}
        public DateTime created { get; set; }=DateTime.UtcNow;
        public DateTime updated { get; set; }
        public int ShippingMethodId{get;set;}
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod? ShippingMethod{get;set;}
        public List<OrderItems>? OrderItems{get;set;}
    }
 }