using AutoMapper;
using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EnvanterApp.Application.Features.Commands.Employees
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommandRequest, GeneralResponse<AddEmployeeCommandResponse>>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AddEmployeeCommandHandler> _logger;
        private readonly IMinioService _minioService;

        public AddEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper, ILogger<AddEmployeeCommandHandler> logger, IMinioService minioService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _minioService = minioService;
        }
        public async Task<GeneralResponse<AddEmployeeCommandResponse>> Handle(AddEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            // Transaction işlemleri yapılacak
            try
            {
                var employee = _mapper.Map<Employee>(request);

                if (await _userManager.FindByEmailAsync(employee.Email) != null)
                    return Response.Fail<AddEmployeeCommandResponse>("Bu email zaten kullanılıyor!", null, HttpStatusCode.BadRequest);

                employee.Status = Domain.Enums.Status.Active;
                employee.CreatedDate = DateTime.Now;
                employee.CreatedBy = Guid.NewGuid();
                employee.UserName = employee.Email;

                if (request.ProfileImage != null && request.ProfileImage.Length > 0)
                {
                    string imageUrl = await _minioService.UploadFileAsync("profile-images", request.ProfileImage);
                    employee.ImageUri = imageUrl;
                }

                var identityResult = await _userManager.CreateAsync(employee, "123456");

                if (!identityResult.Succeeded)
                    return Response.Fail<AddEmployeeCommandResponse>("Personel ekleme işlemi sırasında bir hata oluştu!", null, System.Net.HttpStatusCode.BadRequest);

                return Response.Ok<AddEmployeeCommandResponse>("Personel ekleme işlemi başarılı", null, System.Net.HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Personel ekleme sırasında sistemsel hata: {Message}", ex.Message);
                return Response.Fail<AddEmployeeCommandResponse>("Personel ekleme işlemi sırasında bir hata oluştu!", null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
