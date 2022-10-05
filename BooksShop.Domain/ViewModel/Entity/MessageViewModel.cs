using BooksShop.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace BooksShop.Domain.ViewModel.Entity
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Report { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        [Required]
        public int BookId { get; set; }

        public void Changed(Message mess)
        {
            Report = mess.Report;
            DateChange = mess.DateChange;
            DateCreate = mess.DateCreate;
            UserId = mess.UserId;
            User = mess.User;
            Book = mess.Book;
            BookId = mess.BookId;
        }
    }
}
