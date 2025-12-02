using AutoMapper;
using EnvanterApp.Application.Abstractions.Minio;
using EnvanterApp.Application.DTOs;
using EnvanterApp.Application.Repositories;
using EnvanterApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanterApp.Application.Features.Queries.Categories.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, GeneralResponse<List<GetCategoriesQueryResponse>>>
    {
        private readonly Repositories.IReadRepository<Category> _readRepository;
        private readonly IMapper _mapper;
        private readonly IMinioService _minioService;

        public GetCategoriesQueryHandler(IReadRepository<Category> readRepository, IMapper mapper, IMinioService minioService)
        {
            _readRepository = readRepository;
            _mapper = mapper;
            _minioService = minioService;
        }
        public async Task<GeneralResponse<List<GetCategoriesQueryResponse>>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _readRepository.GetAll().Where(c => c.Status == Domain.Enums.Status.Active).ToListAsync();
                var dtoCategories = _mapper.Map<List<GetCategoriesQueryResponse>>(categories);

                foreach (var category in dtoCategories)
                {
                    if (!string.IsNullOrWhiteSpace(category.ImageUri))
                    {
                        category.ImageUri = await _minioService.GetFileAsync("category-images", category.ImageUri);
                    }
                }

                return Response.Ok<List<GetCategoriesQueryResponse>>("Kategoriler başarıyla getirildi.", dtoCategories, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response.Fail<List<GetCategoriesQueryResponse>>($"Sistemde teknik bir hata oluştu! Hata mesajı : {ex.Message}", null, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
