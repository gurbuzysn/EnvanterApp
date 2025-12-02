using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories.RemoveCategory
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest, GeneralResponse<RemoveCategoryCommandResponse>>
    {
        private readonly IWriteRepository<Category> _writeRepository;

        public RemoveCategoryCommandHandler(IWriteRepository<Category> writeRepository)
        {
            _writeRepository = writeRepository;
        }
        public async Task<GeneralResponse<RemoveCategoryCommandResponse>> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _writeRepository.RemoveAsync(request.Id);

            if(!category)
                return Response.Fail<RemoveCategoryCommandResponse>("Kategori silinemedi!", null, System.Net.HttpStatusCode.NotFound);

            return Response.Ok<RemoveCategoryCommandResponse>("Kategori başarıyla silindi!", null, System.Net.HttpStatusCode.OK);
        }
    }
}
