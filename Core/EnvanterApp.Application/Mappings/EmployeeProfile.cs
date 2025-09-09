using AutoMapper;
using EnvanterApp.Application.Features.Queries.GetEmployees;
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
        }
    }
}
