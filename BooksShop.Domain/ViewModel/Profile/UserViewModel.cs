using BooksShop.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using BooksShop.Domain.Enum;

namespace BooksShop.Domain.ViewModel.Profile
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Role")]
        public string RoleEnum { get; set; }

        [Display(Name = "Login")]
        public string Name { get; set; }

        public List<Book>? FavoriteBook { get; set; } = new();
        public Role Role { get; set; }

        public UserViewModel() { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Role = user.Role;
            RoleEnum = user.Role.ToString();
            

        }
    }
}
