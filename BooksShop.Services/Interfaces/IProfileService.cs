using BooksShop.Domain.Entity;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Profile;

namespace BooksShop.Services.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<Profile>> GetProfile(string userName);

        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}
