using AutoMapper;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQueryRequest, GeneralResponse<List<GetEmployeesQueryResponse>>>
    {
        private readonly IReadRepository<Employee> _readRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeesQueryHandler> _logger;

        public GetEmployeesQueryHandler(IReadRepository<Employee> readRepository, IMapper mapper, ILogger<GetEmployeesQueryHandler> logger)
        {
            _readRepository = readRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GeneralResponse<List<GetEmployeesQueryResponse>>> Handle(GetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            var generalResponse = new GeneralResponse<List<GetEmployeesQueryResponse>>();
            try
            {
                var allEmployees = _readRepository.GetAll().ToList();
                var dtoAllEmployees = _mapper.Map<List<Employee>, List<GetEmployeesQueryResponse>>(allEmployees);

                generalResponse.IsSuccess = true;
                generalResponse.Message = "Çalışanların listesi başarıyla getirildi.";
                generalResponse.Result = dtoAllEmployees;
                _logger.LogInformation($"Employee listesi başarıyla getirildi. Toplam {dtoAllEmployees.Count} kayıt.");
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.IsSuccess = false;
                generalResponse.Message = "Çalışan listesi getirilirken bir hata oluştu!";
                _logger.LogError(ex, "GetEmployeesQuery sırasında bir hata oluştu. Request: {@request}", request);
                return generalResponse;
            }
        }
    }
}





