using AutoMapper;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQueryRequest, GeneralResponse<List<GetEmployeesQueryResponse>>
    {
        private readonly IReadRepository<Employee> _readRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IReadRepository<Employee> readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;

        }
        public async Task<GeneralResponse<List<GetEmployeesQueryResponse>>> Handle(GetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var generalResponse = new GeneralResponse<List<GetEmployeesQueryResponse>>();
            try
            {
                var allEmployees = _readRepository.GetAll().ToList();
                var dtoAllEmployees = _mapper.Map<List<Employee>, List<GetEmployeesQueryResponse>>(allEmployees);

                generalResponse.IsSuccess = true;
                generalResponse.Message = "Employee listesi başarıyla getirildi.";
                generalResponse.Result = dtoAllEmployees;

                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.Message;


                // TO DO: Burada loglama işlemi yapalılacak

                return generalResponse;
            }
        }
    }
}





