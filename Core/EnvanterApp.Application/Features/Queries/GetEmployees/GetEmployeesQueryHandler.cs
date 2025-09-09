using AutoMapper;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQueryRequest, List<GetEmployeesQueryResponse>>
    {
        private readonly IReadRepository<Employee> _readRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IReadRepository<Employee> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeesQueryResponse>> Handle(GetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var allEmployees = _readRepository.GetAll().ToList();

            var dtoAllEmployees = _mapper.Map<List<GetEmployeesQueryResponse>>(allEmployees);

            return dtoAllEmployees;
        }
    }
}
