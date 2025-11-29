using AutoMapper;
using EnvanterApp.Application.Abstractions.Minio;
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
        private readonly IMapper _mapper;
        private readonly IMinioService _minioService;

        public LoginUserQueryHandler(UserManager<Employee> userManager, SignInManager<Employee> signInManager, ITokenHandler tokenHandler, IMapper mapper, IMinioService minioService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
            _minioService = minioService;
        }
        public async Task<GeneralResponse<LoginUserQueryResponse>> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Employee? user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    return Response.Fail<LoginUserQueryResponse>("Sistemde bu bilgilere ait bir kullanıcı bulunmamaktadır!", null, System.Net.HttpStatusCode.Unauthorized);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                    return Response.Fail<LoginUserQueryResponse>("Kullanıcı adı veya şifre hatalı!", null, System.Net.HttpStatusCode.Unauthorized);

                Token token = _tokenHandler.CreateAccessToken();
                var returnUser = _mapper.Map<Employee, LoginUserQueryResponse>(user);
                returnUser.Token = token;
                if (!string.IsNullOrWhiteSpace(returnUser.ImageUri))
                    returnUser.ImageUri = await _minioService.GetFileAsBase64Async("profile-images", returnUser.ImageUri);

                return Response.Ok<LoginUserQueryResponse>("Giriş Başarılı.", returnUser, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response.Fail<LoginUserQueryResponse>($"Sistemde teknik bir hata oluştu. Hata mesajı : {ex.Message}", null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

