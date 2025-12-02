namespace EnvanterApp.Application.Features.Queries.Categories.GetCategories
{
    public class GetCategoriesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUri { get; set; }
    }
}