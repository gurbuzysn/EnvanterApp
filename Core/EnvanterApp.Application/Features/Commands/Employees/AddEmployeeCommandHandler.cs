using AutoMapper;
using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = new GeneralResponse<AddEmployeeCommandResponse>();
            try
            {
                var employee = _mapper.Map<Employee>(request);
                employee.Status = Domain.Enums.Status.Active;
                employee.CreatedDate = DateTime.Now;
                employee.CreatedBy = Guid.Empty;
                employee.UserName = employee.Email;


                if (request.ProfileImage != null && request.ProfileImage.Length > 0)
                {
                    string imageUrl = await _minioService.UploadFileAsync("profile-images", request.ProfileImage);
                    employee.ImageUri = imageUrl;
                }

                var identityResult = await _userManager.CreateAsync(employee, "123456");

                if(identityResult.Succeeded)
                {
                    result.IsSuccess = true;
                    result.Message = "Personel ekleme işlemi başarılı";
                    result.StatusCode = System.Net.HttpStatusCode.OK;
                    return result;
                }

                result.IsSuccess = false;
                result.Message = "Personel ekleme işlemi sırasında bir hata oluştu!";
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Personel ekleme sırasında sistemsel hata: {Message}", ex.Message);

                result.IsSuccess = false;
                result.Message = "Personel ekleme işlemi sırasında bir hata oluştu!";
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }
        }
    }
}
