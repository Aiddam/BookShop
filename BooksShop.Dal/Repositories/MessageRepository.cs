using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;


namespace BooksShop.Dal.Repositories
{
    public class MessageRepository : IBaseRepository<Message>
    {
        private readonly ApplicationDbContext _db;
        public MessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Message> Collection()
        {
            throw new NotImplementedException();
        }

        public async Task Create(Message entity)
        {
            await _db.Messages.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Message entity)
        {
            _db.Messages.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Message> GetAll()
        {
            return _db.Messages;
        }

        public async Task<Message> Update(Message entity)
        {
            _db.Messages.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
