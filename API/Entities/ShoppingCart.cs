using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
namespace API.Entities
{
    public class ShoppingCart : BaseEntity
    {
        [Required]
        public int UserId{get;set;}
        public DateTime Created{get;set;}=DateTime.UtcNow;
        public DateTime Updated{get;set;}
        public ICollection<Product>?Product{get;set;}
    }

}