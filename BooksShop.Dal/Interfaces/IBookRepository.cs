using BooksShop.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksShop.Dal.Interfaces
{
    public interface IBookRepository :IBaseRepository<Book>
    {
        Task<Book> GetByName(string name);
    }
}
