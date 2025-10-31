using EnvanterApp.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Employees.RemoveEmployee
{
    public class RemoveEmployeeCommandRequest : IRequest<GeneralResponse<RemoveEmployeeCommandResponse>>
    {
        public Guid Id { get; set; }
    }
}
