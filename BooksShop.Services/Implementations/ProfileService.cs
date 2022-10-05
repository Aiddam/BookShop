using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;
using BooksShop.Domain.Enum;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Profile;
using BooksShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<User> _userRepository;

        public ProfileService(IBaseRepository<Profile> profileRepository, IBaseRepository<User> userRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }


        public async Task<BaseResponse<Profile>> GetProfile(string userName)
        {
            try
            {
                var books = _userRepository.Collection().Select(x => x.FavoriteBook);
                var user = _userRepository.GetAll().FirstOrDefault(x => x.Name == userName);
                var profile = await _profileRepository.GetAll().FirstOrDefaultAsync(x => x.User.Name == userName);
                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Profile>> Save(ProfileViewModel model)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                                   .FirstOrDefaultAsync(x => x.Id == model.Id);
                profile.ProfileChanged(model);
                await _profileRepository.Update(profile);
                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
