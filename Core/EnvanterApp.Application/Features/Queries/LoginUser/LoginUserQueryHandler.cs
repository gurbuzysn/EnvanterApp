using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, GeneralResponse<LoginUserQueryResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginUserQueryHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<GeneralResponse<LoginUserQueryResponse>> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.UserName);

            if(user == null)
            {
                return new GeneralResponse<LoginUserQueryResponse>() { 
                    IsSuccess = false,
                    Message = "Kullanıcı veya şifre hatalı",
                    Status = System.Net.HttpStatusCode.NotFound 
                };
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) //Authentication Başarılı
            {
                // Burada Authorize yani yetkilendirme işlemleri yapılacak.
                // 1-) Token Ver

            }




    }
}
