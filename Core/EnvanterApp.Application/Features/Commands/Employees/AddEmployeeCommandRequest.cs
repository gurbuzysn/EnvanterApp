using EnvanterApp.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanterApp.Application.Features.Commands.Employees
{
    public class AddEmployeeCommandRequest : IRequest<GeneralResponse<AddEmployeeCommandResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
