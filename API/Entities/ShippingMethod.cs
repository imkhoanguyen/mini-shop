using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace API.Entities
{
    public class ShippingMethod : BaseEntity
    {
        [Required]
        public String Name {get;set;}=null!;
        public double Cost{get;set;}
        public DateTime EstimatedDeliveryTime{get;set;}
        public DateTime Created{get;set;}=DateTime.UtcNow;
        public DateTime Updated{get;set;}
    }


}