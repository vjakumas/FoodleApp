namespace Foodle.Data.Dtos.Recipes
{
        public record RecipeDto(int id, string Name, string Description, DateTime CreationTime, DateTime LastUpdateDate, bool IsEnabled, int CategoryId);
        public record CreateRecipeDto(string Name, string Description, int CategoryId);
        public record UpdateRecipeDto(string ?Name, string ?Description, int CategoryId);
}
