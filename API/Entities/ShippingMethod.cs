using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace API.Entities
{
    public class ShippingMethod : BaseEntity
    {
        public String Name {get;set;}=null!;
        public decimal Cost{get;set;}
        public DateTime EstimatedDeliveryTime{get;set;}
        public DateTime Created{get;set;}=DateTime.UtcNow;
        public DateTime Updated{get;set;}
        
    }


}