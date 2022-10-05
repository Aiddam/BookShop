using BooksShop.Domain.Entity;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Profile;
namespace BooksShop.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUsers();
        Task<BaseResponse<bool>> DeleteUser(int id);
        Task<BaseResponse<User>> GetUser(int? id);
        Task<BaseResponse<User>> GetUser(string? id);
        Task<BaseResponse<User>> Edit(int id,UserViewModel uvm);
        Task<BaseResponse<bool>> CheckedBook(int? id,Book book);
        
    }
}
