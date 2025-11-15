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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Department { get; set; }
        public string? ImageUri { get; set; }
    }
}
