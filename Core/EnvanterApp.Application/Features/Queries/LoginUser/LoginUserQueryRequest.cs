using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryRequest : IRequest<GeneralResponse<LoginUserQueryResponse>>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
