using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;
using BooksShop.Domain.Enum;
using BooksShop.Domain.Helpers;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Account;
using BooksShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace BooksShop.Services.Implementations
{
    public class AccountService :IAccountService
    {
        private readonly Dal.Interfaces.IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Profile> _proFileRepository;

        public AccountService(Dal.Interfaces.IBaseRepository<User> userRepository, IBaseRepository<Profile> proFileRepository)
        {
            _userRepository = userRepository;
            _proFileRepository = proFileRepository;
        }

        public async Task<Domain.Response.IBaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelpers.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<Domain.Response.IBaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Password = HashPasswordHelpers.HashPassword(model.Password),
                };

               

                await _userRepository.Create(user);
                var s =  await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                var profile = new Profile()
                {
                    UserId = s.Id,
                };
                await _proFileRepository.Create(profile);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };

            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

      
    }

}
