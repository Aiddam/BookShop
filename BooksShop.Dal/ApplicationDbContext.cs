using BooksShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;



namespace BooksShop.Dal
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        
    }
}
