using EnvanterApp.Application.DTOs;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryRequest : IRequest<GeneralResponse<List<GetEmployeesQueryResponse>>>
    {
    }
}
