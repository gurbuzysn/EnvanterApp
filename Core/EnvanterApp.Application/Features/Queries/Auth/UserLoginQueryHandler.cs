using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.Auth
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQueryRequest, UserLoginQueryResponse>
    {
      
        public Task<UserLoginQueryResponse> Handle(UserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}







