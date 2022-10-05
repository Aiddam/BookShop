using BooksShop.Domain.Enum;
using BooksShop.Domain.ViewModel.Profile;
using System.ComponentModel.DataAnnotations;


namespace BooksShop.Domain.Entity
{
    public class User
    {
       public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s).*$")]
        public string Password { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
        public List<Book>? FavoriteBook { get; set; } = new();

        public void ChangedBook(UserViewModel uvm)
        {
            FavoriteBook.AddRange(uvm.FavoriteBook);
        }
        
    }
}
