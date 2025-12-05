using AutoMapper;
using EnvanterApp.Application.Features.Commands.Employees.AddEmployee;
using EnvanterApp.Application.Features.Commands.Employees.UpdateEmployee;
using EnvanterApp.Application.Features.Queries.Employees.GetEmployees;
using EnvanterApp.Application.Features.Queries.LoginUser;
using EnvanterApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, GetEmployeesQueryResponse>();
            CreateMap<AddEmployeeCommandRequest, Employee>();
            CreateMap<UpdateEmployeeCommandRequest, Employee>();
            CreateMap<Employee, LoginUserQueryResponse>();

        }
    }
}
