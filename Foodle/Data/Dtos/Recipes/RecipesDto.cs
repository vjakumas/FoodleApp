namespace Foodle.Data.Dtos.Recipes
{
        public record RecipeDto(int id, string Name, string ImageURL, string Description, DateTime CreationTime, DateTime LastUpdateDate, bool IsEnabled, int CategoryId);
        public record CreateRecipeDto(string Name, string? ImageURL, string? Description, int CategoryId);
        public record UpdateRecipeDto(string ?Name, string? ImageURL, string? Description, int CategoryId);
}
