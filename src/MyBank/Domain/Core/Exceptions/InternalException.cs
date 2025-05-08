using System;

namespace Domain.Core.Exceptions
{
  
    public class InternalException : Exception
    {

        public int ErrorCode { get; } = 500;

  
        public object? Details { get; }


        public InternalException(string message)
            : base(message)
        {
            ErrorCode = -1;
            Details = null;
        }

      
        public InternalException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = null;
        }


        public InternalException(string message, int errorCode, object details)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        public InternalException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = -1;
            Details = null;
        }

        public InternalException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            Details = null;
        }

        public InternalException(string message, int errorCode, object details, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}