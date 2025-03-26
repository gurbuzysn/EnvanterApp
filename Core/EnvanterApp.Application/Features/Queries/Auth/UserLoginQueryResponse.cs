using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.Auth
{
    public class UserLoginQueryResponse
    {
        public UserLoginQueryResponse(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }
        public string Token { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
    }
}
