using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helper;

namespace API.Interfaces
{
    public interface IColorRepository
    {
        void AddColor(Color color);
        void UpdateColor(Color color);
        void DeleteColor(Color color);
        Task<Color?> GetColorsById(int id);
        Task<IEnumerable<Color>> GetAllColorsAsync();
        Task<bool> colorExistsAsync(string name);
        Task<PageList<Color>> GetAllColorsAsync(ColorParams colorParams);
    }
}