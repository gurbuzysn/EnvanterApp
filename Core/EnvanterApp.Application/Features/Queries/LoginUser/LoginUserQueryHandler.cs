using EnvanterApp.Application.Abstractions.Token;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, GeneralResponse<LoginUserQueryResponse>>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserQueryHandler(UserManager<Employee> userManager, SignInManager<Employee> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<GeneralResponse<LoginUserQueryResponse>> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            Employee user = await _userManager.FindByEmailAsync(request.UserName);

            if (user == null)
            {
                return new GeneralResponse<LoginUserQueryResponse>()
                {
                    IsSuccess = false,
                    Message = "Kullanıcı veya şifre hatalı",
                    Status = System.Net.HttpStatusCode.Unauthorized
                };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken();

                return new GeneralResponse<LoginUserQueryResponse>()
                {
                    IsSuccess = true,
                    Status = System.Net.HttpStatusCode.OK,
                    Result = new LoginUserQueryResponse() { Token = token }
                };

            }
            return new GeneralResponse<LoginUserQueryResponse>() { IsSuccess = false, Message = "Kullanıcı adı veya şifre hatalı", Status = System.Net.HttpStatusCode.NotFound};
        }
    }
}

