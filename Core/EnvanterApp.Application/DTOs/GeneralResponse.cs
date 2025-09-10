using System.Net;

namespace EnvanterApp.Application.DTOs
{
    public class GeneralResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
