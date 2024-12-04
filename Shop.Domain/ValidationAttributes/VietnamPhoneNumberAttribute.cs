using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Shop.Domain.ValidationAttributes
{
    public class VietnamPhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string phoneNumber = value.ToString();
            string pattern = @"^0\d{9}$";

            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Số điện thoại không hợp lệ.");
        }
    }
}
