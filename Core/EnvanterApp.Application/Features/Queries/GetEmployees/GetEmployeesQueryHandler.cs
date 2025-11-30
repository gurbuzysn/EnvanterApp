using AutoMapper;
using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace EnvanterApp.Application.Features.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQueryRequest, GeneralResponse<List<GetEmployeesQueryResponse>>>
    {
        private readonly IReadRepository<Employee> _readRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeesQueryHandler> _logger;
        private readonly IMinioService _minioService;

        public GetEmployeesQueryHandler(IReadRepository<Employee> readRepository, IMapper mapper, ILogger<GetEmployeesQueryHandler> logger, IMinioService minioService)
        {
            _readRepository = readRepository;
            _mapper = mapper;
            _logger = logger;
            _minioService = minioService;
        }
        public async Task<GeneralResponse<List<GetEmployeesQueryResponse>>> Handle(GetEmployeesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var allEmployees = await _readRepository.GetAll().ToListAsync();
                var dtoAllEmployees = _mapper.Map<List<Employee>, List<GetEmployeesQueryResponse>>(allEmployees);

                foreach (var employee in dtoAllEmployees)
                {
                    if(!employee.ImageUri.IsNullOrEmpty())
                        employee.ProfileImage = await _minioService.GetFileAsync("profile-images", employee.ImageUri!);
                }

                _logger.LogInformation($"Employee listesi başarıyla getirildi. Toplam {dtoAllEmployees.Count} kayıt.");
                return Response.Ok<List<GetEmployeesQueryResponse>>("Çalışanların listesi başarıyla getirildi.", dtoAllEmployees, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "GetEmployeesQuery sırasında bir hata oluştu. Request: {@request}", request);
                return Response.Fail<List<GetEmployeesQueryResponse>>("Çalışan listesi getirilirken bir hata oluştu!",null, HttpStatusCode.InternalServerError);
            }
        }
    }
}





