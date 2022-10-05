using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;


namespace BooksShop.Dal.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Collection()
        {
            var users = _db.Books.Include(x => x.Users).ToList();
            return users;
        }

        public async Task  Create(Book entity)
        {
            await _db.Books.AddAsync(entity);
            await _db.SaveChangesAsync();
            
        }

        public async Task Delete(Book entity)
        {
             _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
           
        }


        public IQueryable<Book> GetAll()
        {
            return _db.Books;
        }


        public async Task<Book> Update(Book entity)
        {
            _db.Books.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
