using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace API.Entities
{
    public class ShoppingCart : BaseEntity
    {
        [Required]
        public DateTime Created{get;set;}= DateTime.UtcNow;
        public DateTime? Updated{get;set;}
        public List<CartItems>?CartItems{get;set;}
    }

}