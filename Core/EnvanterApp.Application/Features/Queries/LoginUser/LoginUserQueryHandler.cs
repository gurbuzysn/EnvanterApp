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
                var generalResponse = new GeneralResponse<LoginUserQueryResponse>();
                generalResponse.IsSuccess = true;
                generalResponse.Message = "Giriş Başarılı.";
                generalResponse.Result = _mapper.Map<Employee, LoginUserQueryResponse>(user);
                generalResponse.Result.Token = token;
                generalResponse.StatusCode = System.Net.HttpStatusCode.OK;

                if(generalResponse.Result.ImageUri != null)
                    generalResponse.Result.ImageUri = await _minioService.GetFileAsBase64Async("profile-images", generalResponse.Result.ImageUri);

                return generalResponse;
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

