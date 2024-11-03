using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.ValidationAttributes
{
    public class MaxFileResolutionAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileResolutionAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                if (file.Length > (1000000 * _maxFileSize)) // convert byte => MB
                    return new ValidationResult(ErrorMessage ?? $"Kích thước của file tối đa là {_maxFileSize} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
