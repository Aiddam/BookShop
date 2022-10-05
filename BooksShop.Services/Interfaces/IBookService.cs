using BooksShop.Domain.Entity;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Entity;
namespace BooksShop.Services.Interfaces
{
    public interface IBookService
    {
        Task<IBaseResponse<IEnumerable<Book>>> GetAllBooks();
        Task<IBaseResponse<Book>> Create(BookViewModel bvm);
        Task<IBaseResponse<Book>> GetBook(int? id);
        Task<IBaseResponse<bool>> Delete(int? id);
        Task<IBaseResponse<Book>> Edit(int id, BookViewModel model);
    }
}
