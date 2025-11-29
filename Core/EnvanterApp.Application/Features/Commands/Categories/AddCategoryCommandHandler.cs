using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;

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
            try
            {
                Category category = new()
                {
                    Name = request.CategoryName,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.Status.Active
                };

                var categoryImageUrl = await _minioService.UploadFileAsync("category-images", request.CategoryImage);
                if (!string.IsNullOrEmpty(categoryImageUrl))
                    category.ImageUri = categoryImageUrl;

                var result = await _writeRepository.AddAsync(category);
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
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Sistemde teknik bir hata oluştu! Hata Mesajı: {ex?.ToString()}";
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
