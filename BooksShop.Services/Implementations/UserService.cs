using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;
using BooksShop.Domain.Enum;
using BooksShop.Domain.Extensions;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Profile;
using BooksShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;


        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<bool>> CheckedBook(int? id, Book book)
        {
            try
            {
                var books = _userRepository.Collection().Select(x => x.FavoriteBook);
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                if (user.FavoriteBook.FirstOrDefault(book)!=null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.OK,
                        Data = true
                    };
                }
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = false
                };


            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                    await _userRepository.Delete(user);
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.OK,
                        Data = true
                    };
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<User>> Edit(int id, UserViewModel uvm)
        {

            try
            {
                var books = _userRepository.Collection().Select(x=>x.FavoriteBook);
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Book not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                
                
                user.ChangedBook(uvm);
                user.Role = uvm.Role;
                await _userRepository.Update(user);
                return new BaseResponse<User>()
                {
                    Data = user,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<User>> GetUser(int? id)
        {

            var baseResponse = new BaseResponse<User>();
            try
            {
                var books = _userRepository.Collection().Select(x => x.FavoriteBook);
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;

                }
                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<User>> GetUser(string? name)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var books =  _userRepository.Collection().Select(x => x.FavoriteBook);
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
                if (user == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;

                }
                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll().Include(x=>x.Profile).ToListAsync();

               
                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

     
    }
}
