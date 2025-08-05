using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace api.DTOs.AccountUser
{
    public class UpdateAccountUserDto : IValidatableObject
    {
        public string Username { get; set; } = string.Empty;

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Regex.IsMatch(Password, @"\d"))
            {
                yield return new ValidationResult(
                    "Password must contain at least one digit.",
                    new[] { nameof(Password) });
            }
        }
    }
}
