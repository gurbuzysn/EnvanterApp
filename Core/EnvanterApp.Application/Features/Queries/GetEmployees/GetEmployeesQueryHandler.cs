using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQueryRequest, List<Employee>>
    {
        private readonly IReadRepository<Employee> _readRepository;
        public GetEmployeesQueryHandler(IReadRepository<Employee> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<Employee>> Handle(GetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var employees = _readRepository.GetAll().ToList();
            return employees;
        }
    }
}
