using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Shop.Application.Repositories;
using Shop.Application.Services.Abstracts;
using Shop.Application.Ultilities;
using Shop.Domain.Entities;

namespace Shop.Application.Services.Implementations
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSize(Size size)
        {
            if (size == null)
                throw new ArgumentNullException(nameof(size), "Size cannot be null.");

            var sizeExists = await _unitOfWork.SizeRepository.SizeExistsAsync(size.Name);
            if (sizeExists)
                throw new InvalidOperationException($"A size with name '{size.Name}' already exists.");

            await _unitOfWork.SizeRepository.AddAsync(size);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Size size)
        {
            if (size == null) throw new ArgumentNullException(nameof(size), "Size cannot be null.");

            var existingSize = await _unitOfWork.SizeRepository.GetSizesById(size.Id);
            if (existingSize == null)
                throw new KeyNotFoundException($"Size with ID {size.Id} not found.");

            await _unitOfWork.SizeRepository.DeleteAsync(size);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Size>> GetAllSizesAsync(bool tracked)
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync(tracked);
            if (sizes == null || !sizes.Any())
                throw new KeyNotFoundException("No sizes found.");

            return sizes;
        }

        public async Task<PagedList<Size>> GetAllSizesAsync(SizeParams sizeParams, bool tracked = false)
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync(sizeParams, tracked);
            if (sizes == null || sizes.Count == 0)
                throw new KeyNotFoundException("No sizes found for the specified parameters.");

            return sizes;
        }

        public async Task<IEnumerable<Size>> GetAllSizesAsync()
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllSizesAsync(true);
            if (sizes == null || !sizes.Any())
                throw new KeyNotFoundException("No sizes found.");

            return sizes;
        }

        public async Task<Size?> GetSizesById(int id)
        {
            var size = await _unitOfWork.SizeRepository.GetSizesById(id);
            if (size == null)
                throw new KeyNotFoundException($"Size with ID {id} not found.");

            return size;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> SizeExistsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Size name cannot be null or empty.", nameof(name));

            return await _unitOfWork.SizeRepository.SizeExistsAsync(name);
        }

        public async Task UpdateAsync(Size size)
        {
            if (size == null)
                throw new ArgumentNullException(nameof(size), "Size cannot be null.");

            var existingSize = await _unitOfWork.SizeRepository.GetSizesById(size.Id);
            if (existingSize == null)
                throw new KeyNotFoundException($"Size with ID {size.Id} not found.");

            await _unitOfWork.SizeRepository.UpdateAsync(size);
            await _unitOfWork.CompleteAsync();
        }
    }
}
