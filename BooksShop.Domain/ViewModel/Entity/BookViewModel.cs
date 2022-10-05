using BooksShop.Domain.Entity;
using Microsoft.AspNetCore.Http;


namespace BooksShop.Domain.ViewModel.Entity
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Genre { get; set; }

        public string Author { get; set; }
        public IFormFile Image { get; set; }
        public List<User>? Users { get; set; }
    }
}
