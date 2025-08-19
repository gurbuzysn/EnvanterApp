using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features
{
    public class GeneralResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
