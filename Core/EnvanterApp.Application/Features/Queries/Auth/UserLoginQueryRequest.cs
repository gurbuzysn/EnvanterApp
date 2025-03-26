using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.Auth
{
    public class UserLoginQueryRequest : IRequest<UserLoginQueryResponse>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
