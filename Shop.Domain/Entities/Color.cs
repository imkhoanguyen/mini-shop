
namespace Shop.Domain.Entities
{
    public class Color : BaseEntity
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool IsDelete { get; set; } = false;

       
    }
}
