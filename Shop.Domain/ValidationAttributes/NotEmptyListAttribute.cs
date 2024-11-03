using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.ValidationAttributes
{
    public class NotEmptyListAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as System.Collections.IList;
            if (list != null && list.Count > 0)
                return ValidationResult.Success;
            return new ValidationResult(ErrorMessage ?? "Danh sách không được rỗng");
        }
    }
}
