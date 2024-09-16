using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;

namespace API.Helper
{
    public class SizeParams : PaginationParams
    {
        public string? SearchString { get; set; }
    }
}