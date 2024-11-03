namespace Shop.Domain.Entities
{
    public class Size : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsDelete { get; set; } = false;
        
    }
}
