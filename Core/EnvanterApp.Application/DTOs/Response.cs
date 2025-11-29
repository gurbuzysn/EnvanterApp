using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.DTOs
{
    public static class Response
    {
        public static GeneralResponse<T> Ok<T>(string message, T? result = default, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new()
            {
                IsSuccess = true,
                Message = message,
                Result = result,
                StatusCode = statusCode
            };

        public static GeneralResponse<T> Fail<T>(string message, T? result = default, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new()
            {
                IsSuccess = false,
                Message = message,
                Result = result,
                StatusCode = statusCode
            };
    }
}
