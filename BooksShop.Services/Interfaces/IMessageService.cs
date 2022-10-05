using BooksShop.Domain.Entity;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Entity;
namespace BooksShop.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IBaseResponse<IEnumerable<Message>>> GetAllMessage();
        Task<IBaseResponse<Message>> Create(MessageViewModel mvm);
        Task<IBaseResponse<Message>> GetMessage(int? id);
        Task<IBaseResponse<bool>> Delete(int? id);
        Task<IBaseResponse<Message>> Edit(int id, MessageViewModel model);
    }
}
