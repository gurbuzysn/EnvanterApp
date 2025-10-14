using AutoMapper;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Employees
{
    public class AddEmployeCommandHandler : IRequestHandler<AddEmployeeCommandRequest, GeneralResponse<AddEmployeeCommandResponse>>
    {
        private readonly IWriteRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public AddEmployeCommandHandler(IWriteRepository<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<AddEmployeeCommandResponse>> Handle(AddEmployeeCommandRequest request, CancellationToken cancellationToken)
        {

            //Amaç => Gelen Personeli veritabanına kaydetmek.

            try
            {
                var employee = _mapper.Map<Employee>(request);
                employee.Status = Domain.Enums.Status.Active;
                employee.CreatedDate = DateTime.Now;
                employee.CreatedBy = Guid.Empty;

                var result = await _repository.AddAsync(employee);
                await _repository.SaveAsync();



                return new GeneralResponse<AddEmployeeCommandResponse>
                {
                    IsSuccess = true,
                    Message = result ? "Personel Ekleme İşlemi Başarılı" : "Personel Ekleme İşlemi Başarısız",
                    Result = _mapper.Map<AddEmployeeCommandResponse>(employee),
                    StatusCode = System.Net.HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {

                throw;
            }

            

           






        }
    }
}
