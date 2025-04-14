using EnvanterApp.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        public Application.DTOs.Token CreateAccessToken()
        {
            Application.DTOs.Token token = new();


        }
    }
}
 