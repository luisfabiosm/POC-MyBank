
using System;

namespace Domain.Core.Exceptions
{
  
    public class ValidateException : Exception
    {

        public int ErrorCode { get; } = -1;

  
        public List<ErrorDetails> Errors { get; private set; }

        public ValidateException()
        {
            this.Errors = new List<ErrorDetails>();
        }

        public ValidateException(ErrorDetails errordatails)
        {
            this.Errors = new List<ErrorDetails>();
            this.Errors.Add(errordatails);
        }

        public void AddDetails(ErrorDetails details)
        {
            this.Errors.Add(details);
        }

        public ValidateException(string message)
            : base(message)
        {
            ErrorCode = -1;
            this.Errors = new List<ErrorDetails>();
        }

        public ValidateException(string message, int errorCode, object details)
           : base(message)
        {
            ErrorCode = errorCode;
            Errors = (List<ErrorDetails>)details;
        }


        public ValidateException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
            this.Errors = new List<ErrorDetails>();
        }


      
        public ValidateException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = -1;
            this.Errors = new List<ErrorDetails>();
        }

        public ValidateException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            this.Errors = new List<ErrorDetails>();
        }

       
    }
}