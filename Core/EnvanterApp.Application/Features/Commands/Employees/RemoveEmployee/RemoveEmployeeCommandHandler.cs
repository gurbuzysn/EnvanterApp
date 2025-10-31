using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Employees.RemoveEmployee
{
    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommandRequest, GeneralResponse<RemoveEmployeeCommandResponse>>
    {
        private readonly UserManager<Employee> _userManager;

        public RemoveEmployeeCommandHandler(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GeneralResponse<RemoveEmployeeCommandResponse>> Handle(RemoveEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var result = new GeneralResponse<RemoveEmployeeCommandResponse>();

            var employee = await _userManager.FindByIdAsync(request.Id.ToString());

            if (employee == null)
            {
                result.IsSuccess = false;
                result.Message = "Personel bulunamadı!";
                result.StatusCode = System.Net.HttpStatusCode.NotFound;
                return result;
            }

            var deletedEmployee = await _userManager.DeleteAsync(employee);

            if (!deletedEmployee.Succeeded)
            {
                result.IsSuccess = false;
                result.Message = "Personel silinirken bir hata oluştu.";
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }

            result.IsSuccess = true;
            result.Message = "Personel başarıyla silindi.";
            result.Result = new RemoveEmployeeCommandResponse();
            result.StatusCode = System.Net.HttpStatusCode.OK;
            return result;
        }
    }
}
