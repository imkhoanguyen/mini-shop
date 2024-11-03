using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Parameters
{
    public class BaseParams : PaginationParams
    {
        public string Search { get; set; } = string.Empty;
        public virtual string OrderBy { get; set; } = "id_desc";
    }
}
