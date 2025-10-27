using EnvanterApp.Application.Abstractions.Token;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;


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
            Employee user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return new GeneralResponse<LoginUserQueryResponse>()
                {
                    IsSuccess = false,
                    Message = "Kullanıcı veya şifre hatalı",
                };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken();

                return new GeneralResponse<LoginUserQueryResponse>()
                {
                    IsSuccess = true,
                    Message = "Giriş Başarılı.",
                    Result = new LoginUserQueryResponse() { Token = token },
                    StatusCode = System.Net.HttpStatusCode.OK
                };

            }
            return new GeneralResponse<LoginUserQueryResponse>()
            {
                IsSuccess = false,
                Message = "Kullanıcı adı veya şifre hatalı",
                StatusCode = System.Net.HttpStatusCode.Unauthorized
            };
        }
    }
}

