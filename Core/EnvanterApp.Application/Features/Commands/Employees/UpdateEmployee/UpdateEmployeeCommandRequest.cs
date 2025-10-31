using EnvanterApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandRequest : IRequest<GeneralResponse<UpdateEmployeeCommandResponse>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
