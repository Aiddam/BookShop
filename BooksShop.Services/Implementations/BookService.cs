using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;

using BooksShop.Domain.Enum;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Entity;
using BooksShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BooksShop.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public BookService(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Book>>> GetAllBooks()
        {
            var baseResponse = new BaseResponse<IEnumerable<Book>>();
            try
            {
                var books =  _bookRepository.GetAll();
                if (books.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов!";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = books;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return  baseResponse;
            }
            catch (Exception ex)
            {
                return  new BaseResponse<IEnumerable<Book>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Book>> GetBook(int? id)
        {
            var baseResponse = new BaseResponse<Book>();
            try
            {
                var book = await _bookRepository.GetAll().Include( x => x.Messages).ThenInclude(x=>x.User).ThenInclude(x=>x.Profile).FirstOrDefaultAsync(x=>x.Id==id);
                if (book == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;

                }
                baseResponse.Data = book;
                return baseResponse;
            }
            catch (Exception ex)
            {
               
                return new BaseResponse<Book>()
                {
                    Description = $"[GetBook] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<Domain.Response.IBaseResponse<Book>> GetBookByName(string name)
        {
            var baseResponse = new BaseResponse<Book>();
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Title == name);
                if (book == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;

                }
                baseResponse.Data = book;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Book>()
                {
                    Description = $"[GetBookByName] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<Domain.Response.IBaseResponse<bool>> Delete (int? id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==id);
                if (book ==null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }
               
                await _bookRepository.Delete(book);
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Delete] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Book>> Create(BookViewModel bvm)
        {
            try
            {
                var book = new Book(bvm);
                await _bookRepository.Create(book);
                return new BaseResponse<Book>()
                {
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Data = book
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Book>()
                {
                    Description = $"[CreateBook] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<Domain.Response.IBaseResponse<Book>> Edit(int id, BookViewModel model)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<Book>()
                    {
                        Description = "Book not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                book.Changed(model);
                await _bookRepository.Update(book);
                return new BaseResponse<Book>()
                {
                    Data = book,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Book>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
