using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryRequest : IRequest<GeneralResponse<LoginUserQueryResponse>>
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = null!;
        [JsonPropertyName("Password")]
        public string Password { get; set; } = null!;
    }
}
