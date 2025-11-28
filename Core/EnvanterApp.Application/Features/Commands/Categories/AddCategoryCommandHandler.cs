using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Commands.Categories
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommandRequest, GeneralResponse<AddCategoryCommandResponse>>
    {
        private readonly IWriteRepository<Category> _writeRepository;
        private readonly IMinioService _minioService;

        public AddCategoryCommandHandler(IWriteRepository<Category> writeRepository, IMinioService minioService)
        {
            _writeRepository = writeRepository;
            _minioService = minioService;
        }
        public async Task<GeneralResponse<AddCategoryCommandResponse>> Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            GeneralResponse<AddCategoryCommandResponse> response = new();
            Category category = new()
            {
                Name = request.CategoryName,
                CreatedDate = DateTime.Now,
                Status = Domain.Enums.Status.Active
            };

            var categoryImageUrl = _minioService.UploadFileAsync("category-images", request.CategoryImage).Result;
            if (string.IsNullOrEmpty(categoryImageUrl))
                category.ImageUri = categoryImageUrl;

            var result = _writeRepository.AddAsync(category).Result;
            var saveResult = await _writeRepository.SaveAsync();
            if (saveResult < 1)
            {
                response.IsSuccess = false;
                response.Message = "Kategori ekleme işlemi başarısız";
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            response.IsSuccess = true;
            response.Message = "Kategori ekleme işlemi başarılı";
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
    }
}
