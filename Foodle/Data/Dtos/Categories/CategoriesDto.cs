namespace Foodle.Data.Dtos.Categories
{
    public record CategoryDto(int id, string Name, string Description, DateTime CreationTime, DateTime LastUpdateDate, bool IsEnabled);
    public record CreateCategoryDto(string Name, string Description);
    public record UpdateCategoryDto(string Name, string Description);
}
