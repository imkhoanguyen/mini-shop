namespace API.Helpers
{
    public class ProductParams : PaginationParams
    {
        public string? SearchString { get; set; }
        public int SelectedSize { get; set; }
        public int SelectedColor { get; set; }
    }
}