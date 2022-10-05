using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Account;
using System.Security.Claims;


namespace BooksShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
