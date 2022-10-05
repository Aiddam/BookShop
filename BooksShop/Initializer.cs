using BooksShop.Dal.Interfaces;
using BooksShop.Dal.Repositories;
using BooksShop.Domain.Entity;
using BooksShop.Services.Implementations;
using BooksShop.Services.Interfaces;

namespace BooksShop
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Book>, BookRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            services.AddScoped<IBaseRepository<Message>, MessageRepository>();

        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
