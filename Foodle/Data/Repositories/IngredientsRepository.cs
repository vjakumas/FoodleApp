using Foodle.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Foodle.Data.Repositories
{
    public interface IIngredientsRepository
    {
        Task<Ingredient?> GetAsync(int recipeId, int ingredientId);
        Task<Ingredient?> GetIngredientAsync(int ingredientId);
        Task<IReadOnlyList<Ingredient>> GetManyAsync(int recipeId);
        Task<IReadOnlyList<Ingredient>> GetManyAsync();
        Task CreateAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(Ingredient ingredient);
    }

    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly FoodleDbContext _context;

        public IngredientsRepository(FoodleDbContext foodleDbContext)
        {
            _context = foodleDbContext;
        }

        public async Task<Ingredient?> GetIngredientAsync(int ingredientId)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(x => x.Id == ingredientId);
        }

        public async Task<Ingredient?> GetAsync(int recipeId, int ingredientId)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(x => x.Id == ingredientId && x.RecipeId == recipeId);
        }

        public async Task<IReadOnlyList<Ingredient>> GetManyAsync(int recipeId)
        {
            return await _context.Ingredients.Where(x => x.RecipeId == recipeId).ToListAsync();
        }

        public async Task<IReadOnlyList<Ingredient>> GetManyAsync()
        {
            return await _context.Ingredients.Where(x => x.RecipeId != 0).ToListAsync();
        }

        public async Task CreateAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
