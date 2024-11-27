using Shop.Application.Parameters;

namespace API.Helpers
{
    public class ProductParams : BaseParams
    {
        public List<int>? SelectedSize { get; set; }
        public int SelectedColor { get; set; }
        public List<int>? SelectedCategory { get; set; }
    }
}