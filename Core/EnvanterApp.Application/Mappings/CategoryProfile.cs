using AutoMapper;
using EnvanterApp.Application.Features.Queries.Categories.GetCategories;
using EnvanterApp.Domain.Entities;

namespace EnvanterApp.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoriesQueryResponse>();
        }
    }
}
