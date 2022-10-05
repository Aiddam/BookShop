using BooksShop.Domain.ViewModel.Entity;
using System.ComponentModel.DataAnnotations;


namespace BooksShop.Domain.Entity
{
    public class Message
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
        
        public int BookId { get; set; }

        public Message()
        {

        }
        public Message(MessageViewModel mvm)
        {
            Changed(mvm);
        }
        public void Changed(MessageViewModel mvm)
        {
            Id = mvm.Id;
            Report = mvm.Report;
            DateCreate = mvm.DateCreate;
            DateChange = mvm.DateChange;
            User = mvm.User;
            UserId = mvm.UserId;
            Book = mvm.Book;
            BookId = mvm.BookId;
        }
    }
}
