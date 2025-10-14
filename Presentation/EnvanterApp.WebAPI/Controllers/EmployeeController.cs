using EnvanterApp.Application.Features.Commands.Employees;
using EnvanterApp.Application.Features.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanterApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetEmployeesQueryRequest());
            return Ok(employees);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeCommandRequest addEmployeeCommandRequest)
        {
            var result = await _mediator.Send(addEmployeeCommandRequest);
           

            return Ok(result);
        }

    }
}
