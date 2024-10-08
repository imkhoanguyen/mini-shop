using API.Entities;
using API.Helpers;

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