using BooksShop.Dal.Interfaces;
using BooksShop.Domain.Entity;
using BooksShop.Domain.Enum;
using BooksShop.Domain.Response;
using BooksShop.Domain.ViewModel.Entity;
using BooksShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BooksShop.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository<Message> _messageRepository;

        public MessageService(IBaseRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IBaseResponse<Message>> Create(MessageViewModel mvm)
        
        {
            try
            {
                var message = new Message(mvm);
                await _messageRepository.Create(message);
                return new BaseResponse<Message>()
                {
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Data = message
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Message>()
                {
                    Description = $"[CreateBook] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int? id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var message = await _messageRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (message == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;
                }

                await _messageRepository.Delete(message);
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

        public async Task<IBaseResponse<Message>> Edit(int id, MessageViewModel model)
        {
            try
            {
                var message = await _messageRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (message == null)
                {
                    return new BaseResponse<Message>()
                    {
                        Description = "Book not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                message.Changed(model);
                await _messageRepository.Update(message);
                return new BaseResponse<Message>()
                {
                    Data = message,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Message>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Message>>> GetAllMessage()
        {
            var baseResponse = new BaseResponse<IEnumerable<Message>>();
            try
            {
                var messages = _messageRepository.GetAll().Include(x=>x.User);
                if (messages.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов!";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = messages;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Message>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Message>> GetMessage(int? id)
        {
            var baseResponse = new BaseResponse<Message>();
            try
            {
                var message = await _messageRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (message == null)
                {
                    baseResponse.Description = "User Not Found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    return baseResponse;

                }
                baseResponse.Data = message;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Message>()
                {
                    Description = $"[GetBook] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
