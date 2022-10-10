namespace Foodle.Data.Dtos.Ingredients
{
    public record IngredientDto(int id, string Name, string Description, int Amount, string Measurement, int RecipeId);
    public record CreateIngredientDto(string Name, string Description, int Amount, string Measurement);
    public record UpdateIngredientDto(string Name, string Description, int Amount, string Measurement);
}
