using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, GeneralResponse<LoginUserQueryResponse>>
    {
        public Task<GeneralResponse<LoginUserQueryResponse>> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {






            throw new NotImplementedException();
        }
    }
}
