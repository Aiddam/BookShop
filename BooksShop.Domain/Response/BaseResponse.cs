using BooksShop.Domain.Enum;

namespace BooksShop.Domain.Response
{
    public class BaseResponse<T> :IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode  StatusCode {get;set; }
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    {
        string Description { get; }
        public T Data { get; }
        public StatusCode StatusCode { get; }
    }
}
