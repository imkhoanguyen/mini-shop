namespace API.Helpers
{
    public class ProductParams : PaginationParams
    {
        public string? SortOrder { get; set; }
        public string? SearchString { get; set; }
        public List<int> SelectedCategory { get; set; } = [];
        public string? SelectedCategoryString { get; set; }
        public string? SelectedPriceRange { get; set; }
        public int SelectedSize { get; set; }
        public int SelectedColor { get; set; }
        public bool Shop { get; set; } = false;
    }
}