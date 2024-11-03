using Shop.Application.Parameters;

namespace API.Helpers
{
    public class ProductParams : BaseParams
    {
        public int SelectedSize { get; set; }
        public int SelectedColor { get; set; }
    }
}