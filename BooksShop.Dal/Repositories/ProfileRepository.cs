using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;

namespace BooksShop.Dal.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Profile> Collection()
        {
            //TODO:ProfileCollection
            throw new NotImplementedException();
        }

        public async Task Create(Profile entity)
        {
           await _dbContext.Profiles.AddAsync(entity);
           await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Profile entity)
        {
             _dbContext.Profiles.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            
            return _dbContext.Profiles;
        }

        public Task Remove(Profile Entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> Update(Profile entity)
        {
             _dbContext.Profiles.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
