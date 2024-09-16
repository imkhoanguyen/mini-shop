using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;

namespace API.Interfaces
{
    public interface ISizeRepository
    {
        void AddSize(Size size);
        void UpdateSize(Size size);
        void DeleteSize(Size size);
        Task<Size?> GetSizesById(int id);
        Task<IEnumerable<Size>> GetAllSizesAsync();
        Task<bool> SizeExistsAsync(string name);
        Task<PageList<Size>> GetAllSizesAsync(SizeParams sizeParams);

    }
}