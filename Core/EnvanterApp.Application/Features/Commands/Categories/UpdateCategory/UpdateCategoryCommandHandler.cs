using AutoMapper;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, GeneralResponse<UpdateCategoryCommandResponse>>
    {
        private readonly IWriteRepository<Category> _writeRepository;
        private readonly IReadRepository<Category> _readRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IWriteRepository<Category> writeRepository, IReadRepository<Category> readRepository, IMapper mapper)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _readRepository.GetWhere(c => c.Id == request.Id).FirstOrDefault();

                if (category != null)
                {
                    var updatedCategory = _mapper.Map(request, category);
                    updatedCategory.ModifiedDate = DateTime.Now;
                    _writeRepository.Update(updatedCategory);
                    _writeRepository.SetPropertyNotModified(category, x => x.RowId);
                    await _writeRepository.SaveAsync();
                    return Response.Ok<UpdateCategoryCommandResponse>("Kategori Başarıyla Güncellendi.", null, System.Net.HttpStatusCode.OK);
                }


                return Response.Fail<UpdateCategoryCommandResponse>("Kategori Bulunamadı.", null, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Response.Fail<UpdateCategoryCommandResponse>($"Sistemde teknik hata. Hata mesajı : {ex.Message}", null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
