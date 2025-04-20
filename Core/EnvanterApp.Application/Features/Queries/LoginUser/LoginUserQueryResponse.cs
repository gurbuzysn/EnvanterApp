using EnvanterApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryResponse
    {
        public Token Token { get; set; } = null!;
    }
}
