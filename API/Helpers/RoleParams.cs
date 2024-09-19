using API.Helpers;

namespace API.Helper
{
    public class RoleParams : PaginationParams
    {
        public string? Query { get; set; }
        public string OrderBy { get; set; } = "name";
    }
}
