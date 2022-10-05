using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksShop.Dal.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task  Create(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        IQueryable<T> GetAll();
        IEnumerable<T> Collection();
       
    }
}
