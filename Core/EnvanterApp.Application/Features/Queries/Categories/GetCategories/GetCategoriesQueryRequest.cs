using EnvanterApp.Application.DTOs;
using MediatR;

namespace EnvanterApp.Application.Features.Queries.Categories.GetCategories
{
    public class GetCategoriesQueryRequest : IRequest<GeneralResponse<GetCategoriesQueryResponse>>
    {
    }
}
