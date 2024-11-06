using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Shop.Application.Parameters;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;


namespace Shop.Application.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddColor(Color Color)
        {
            if (Color == null)
                throw new ArgumentNullException(nameof(Color), "Color cannot be null.");

            var ColorExists = await _unitOfWork.ColorRepository.colorExistsAsync(Color.Name);
            if (ColorExists)
                throw new InvalidOperationException($"A Color with name '{Color.Name}' already exists.");

            await _unitOfWork.ColorRepository.AddAsync(Color);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ColorExistsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Color name cannot be null or empty.", nameof(name));

            return await _unitOfWork.ColorRepository.colorExistsAsync(name);
        }

        public async Task DeleteAsync(Color Color)
        {
            if (Color == null) throw new ArgumentNullException(nameof(Color), "Color cannot be null.");

            var existingColor = await _unitOfWork.ColorRepository.GetColorsById(Color.Id);
            if (existingColor == null)
                throw new KeyNotFoundException($"Color with ID {Color.Id} not found.");

            await _unitOfWork.ColorRepository.DeleteAsync(Color);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync(bool tracked)
        {
            var Colors = await _unitOfWork.ColorRepository.GetAllColorsAsync(tracked);
            if (Colors == null || !Colors.Any())
                throw new KeyNotFoundException("No Colors found.");

            return Colors;
        }

        public async Task<PagedList<Color>> GetAllColorsAsync(ColorParams ColorParams, bool tracked = false)
        {
            var Colors = await _unitOfWork.ColorRepository.GetAllColorsAsync(ColorParams, tracked);
            if (Colors == null || Colors.Count == 0)
                throw new KeyNotFoundException("No Colors found for the specified parameters.");

            return Colors;
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync()
        {
            var Colors = await _unitOfWork.ColorRepository.GetAllColorsAsync(true);
            if (Colors == null || !Colors.Any())
                throw new KeyNotFoundException("No Colors found.");

            return Colors;
        }

        public async Task<Color?> GetColorsById(int id)
        {
            var Color = await _unitOfWork.ColorRepository.GetColorsById(id);
            if (Color == null)
                throw new KeyNotFoundException($"Color with ID {id} not found.");

            return Color;
        }

        public async Task UpdateAsync(Color Color)
        {
            if (Color == null)
                throw new ArgumentNullException(nameof(Color), "Color cannot be null.");

            var existingColor = await _unitOfWork.ColorRepository.GetColorsById(Color.Id);
            if (existingColor == null)
                throw new KeyNotFoundException($"Color with ID {Color.Id} not found.");

            await _unitOfWork.ColorRepository.UpdateAsync(Color);
            await _unitOfWork.CompleteAsync();
        }
    }
}