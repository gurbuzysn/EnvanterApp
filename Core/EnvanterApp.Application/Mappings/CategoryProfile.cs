using AutoMapper;
using EnvanterApp.Application.Features.Commands.Categories.UpdateCategory;
using EnvanterApp.Application.Features.Queries.Categories.GetCategories;
using EnvanterApp.Domain.Entities;

namespace EnvanterApp.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoriesQueryResponse>();
            CreateMap<UpdateCategoryCommandRequest, Category>()
                .ForMember(dest => dest.RowId, opt => opt.Ignore());
        }
    }
}
