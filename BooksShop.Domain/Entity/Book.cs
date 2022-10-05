using BooksShop.Domain.ViewModel.Entity;
using System.ComponentModel.DataAnnotations;


namespace BooksShop.Domain.Entity
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [MinLength(3)]
        [MaxLength(30)]
        public string? Genre { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Author { get; set; }
        
        public byte[] Image { get; set; }
        public List<User>? Users { get; set; }
        public List<Message>? Messages { get; set; }
        private int SelectId { get; set; }
       
        public Book()  { }
        public Book(BookViewModel book)
        {
            Changed(book);
        }
        public void Changed(BookViewModel book)
        {
            Id = book.Id;
            Title = book.Title;
            Description = book.Description;
            ReleaseDate = book.ReleaseDate;
            Genre = book.Genre;
            Author = book.Author;
            ImageChanged(book);
            Users = book.Users;
        }
        public void ImageChanged(BookViewModel bvm)
        {
            if (bvm.Image !=null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(bvm.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)bvm.Image.Length);
                }
                Image = imageData;
            }
            else
            {
                return;
            }
        }

    }
}
