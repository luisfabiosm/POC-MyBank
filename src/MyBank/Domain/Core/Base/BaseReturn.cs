using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Core.Base
{
    public record BaseReturn<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }
        public string CorrelationId { get; set; }

        public bool IsSuccess => Success;

        public BaseReturn() { }

        public BaseReturn(T data, bool success = true, string message = "Success")
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public BaseReturn(T data, bool success, string message, string correlationId)
        {
            Success = success;
            Message = message;
            Data = data;
            CorrelationId = correlationId;
        }

        public BaseReturn(Exception exception, bool includeDetails = false, string correlationId = null)
        {
            Success = false;
            CorrelationId = correlationId;

            switch (exception)
            {
                case BusinessException businessEx:
                    Message = businessEx.Message;
                    ErrorCode = businessEx.ErrorCode;
                    Data = includeDetails ? (T)businessEx.Details : default;
                    break;
                case InternalException internalEx:
                    Message = internalEx.Message;
                    ErrorCode = internalEx.ErrorCode;
                    Data = includeDetails ? (T)internalEx.Details : default;
                    break;
                case ValidateException validateEx:
                    Message = validateEx.Message;
                    ErrorCode = validateEx.ErrorCode;
                    Data = validateEx.Errors;
                    break;
                default:
                    Message = exception.Message;
                    ErrorCode = -1;
                    break;
            }
        }

        public static BaseReturn<T> FromSuccess(T data, string message = "Success", string correlationId = null)
        {
            return new BaseReturn<T>
            {
                Success = true,
                Message = message,
                Data = data,
                CorrelationId = correlationId
            };
        }

        public static BaseReturn<T> FromError(string message, int errorCode = -1, string correlationId = null)
        {
            return new BaseReturn<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                CorrelationId = correlationId
            };
        }

        // Convert BaseReturn to appropriate exception based on its content
        public void ThrowIfError()
        {
            if (!Success)
            {
                switch (ErrorCode)
                {
                    case 400:
                        throw new BusinessException(Message, ErrorCode, Data);

                    case -1:
                        throw new ValidateException(Message, ErrorCode, Data);

                    default:
                        throw new InternalException(Message, ErrorCode, Data);
                        break;
                }
            }
        }
    }

}