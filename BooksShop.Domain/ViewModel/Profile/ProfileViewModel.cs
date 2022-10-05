using BooksShop.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BooksShop.Domain.ViewModel.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Range(0, 100, ErrorMessage = "Range 0 to 100")]
        public int? Age { get; set; }
        public IFormFile? Avatar { get; set; }
        public string UserName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public User? User { get; set; }

        

    }
}
