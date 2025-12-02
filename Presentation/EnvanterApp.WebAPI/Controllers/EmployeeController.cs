using EnvanterApp.Application.Features.Commands.Employees;
using EnvanterApp.Application.Features.Commands.Employees.RemoveEmployee;
using EnvanterApp.Application.Features.Commands.Employees.UpdateEmployee;
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
        public async Task<IActionResult> AddEmployee([FromForm] AddEmployeeCommandRequest addEmployeeCommandRequest)
        {
            var result = await _mediator.Send(addEmployeeCommandRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RemoveEmployeeCommandRequest() { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromForm] UpdateEmployeeCommandRequest updateEmployeeCommandRequest)
        {
            updateEmployeeCommandRequest.Id = id;
            var result = await _mediator.Send(updateEmployeeCommandRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
