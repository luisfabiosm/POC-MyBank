using System;

namespace Domain.Core.Exceptions
{
  
    public class BusinessException : Exception
    {

        public int ErrorCode { get; } = 400;

  
        public object? Details { get; }


        public BusinessException(string message)
            : base(message)
        {
            ErrorCode = -1;
            Details = null;
        }

      
        public BusinessException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = null;
        }


        public BusinessException(string message, int errorCode, object details)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = -1;
            Details = null;
        }

        public BusinessException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            Details = null;
        }

        public BusinessException(string message, int errorCode, object details, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}