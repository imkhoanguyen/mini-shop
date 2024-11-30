using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Shop.Domain.ValidationAttributes
{
    public class VietnamPhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string phoneNumber = value.ToString();
            string pattern = @"^(0|\+84)(3[2-9]|5[6|8|9]|7[0-9]|8[1-5]|9[0-9])[0-9]{7}$";

            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Số điện thoại không hợp lệ.");
        }
    }
}
