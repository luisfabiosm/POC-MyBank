namespace Domain.Core.Base
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
  

        public static BaseResponse<T> CreateSuccess(T data, string message = "Sucesso")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static BaseResponse<T> CreateError(string message, int? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode??-1
            };
        }
    }
}
