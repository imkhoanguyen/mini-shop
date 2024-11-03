using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                    return new ValidationResult(ErrorMessage ?? GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            string errorMessage = "Vui lòng chọn file với các định dạng sau: ";
            foreach (var extension in _extensions)
            {
                errorMessage += extension + "; ";
            }
            return errorMessage;
        }
    }
}
