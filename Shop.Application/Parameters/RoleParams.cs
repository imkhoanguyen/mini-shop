using Shop.Application.Parameters;

namespace API.Helper
{
    public class RoleParams : BaseParams
    {
        public string? Query { get; set; }
        public override string OrderBy { get; set; } = "created_desc";
    }
}
