using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;

namespace EnvanterApp.Application.Features.Commands.Categories.AddCategory
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

                await _writeRepository.AddAsync(category);
                var saveResult = await _writeRepository.SaveAsync();

                 if (saveResult < 1)
                    return Response.Fail<AddCategoryCommandResponse>("Kategori ekleme işlemi başarısız!", null, System.Net.HttpStatusCode.BadRequest);

                return Response.Ok<AddCategoryCommandResponse>("Kategori ekleme işlemi başarıyla gerçekleşti.", null, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response.Fail<AddCategoryCommandResponse>($"Sistemde teknik bir hata oluştu! Hata Mesajı: {ex?.Message}", null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
