using AutoMapper;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommandRequest, GeneralResponse<UpdateEmployeeCommandResponse>>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<UpdateEmployeeCommandResponse>> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
                var result = new GeneralResponse<UpdateEmployeeCommandResponse>();
            try
            {
                Employee employee = await _userManager.FindByIdAsync(request.Id.ToString());

                if(employee == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Kullanıcı Bulunamadı!";
                    return result;
                }

                var updateUser = _mapper.Map(request, employee);
                var identityResult = await _userManager.UpdateAsync(updateUser);

                if (!identityResult.Succeeded)
                {
                    result.IsSuccess = false;
                    result.Message = "Kullanıcı eklenirken bir hata oluştu!";
                    return result;
                }

                result.IsSuccess = true;
                result.Message = "Kullanıcı başarıyla güncellendi.";
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Sistemsel bir hata oluştu!";
                return result;
            }
        }
    }
}
