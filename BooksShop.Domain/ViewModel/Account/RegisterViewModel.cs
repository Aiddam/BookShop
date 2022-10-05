using System.ComponentModel.DataAnnotations;

namespace BooksShop.Domain.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter name")]
        [MaxLength(20, ErrorMessage = "The name must be less than 20 characters long")]
        [MinLength(3, ErrorMessage = "The name must be longer than 3 characters")]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9-_\\.]{1,20}$",ErrorMessage = "The first character must be a letter, Cyrillic is not available")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).*$",ErrorMessage = "The password must be Latin characters, have at least one uppercase and lowercase letter, and have numbers.")]
        [Required(ErrorMessage = "Enter Password")]
        [MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmed password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]

        public string PasswordConfirm { get; set; }
    }
}
