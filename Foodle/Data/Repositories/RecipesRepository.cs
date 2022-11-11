using Foodle.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foodle.Data.Repositories
{
    public interface IRecipesRepository
    {
        Task<Recipe?> GetByCategoryAsync(int recipeId, int categoryId);
        Task<Recipe?> GetAsync(int recipeId);
        Task<IReadOnlyList<Recipe>> GetManyByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Recipe>> GetManyAsync();
        Task CreateAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(Recipe recipe);
    }

    public class RecipesRepository : IRecipesRepository
    {
        private readonly FoodleDbContext _context;

        public RecipesRepository(FoodleDbContext foodleDbContext)
        {
            _context = foodleDbContext;
        }

        public async Task<Recipe?> GetByCategoryAsync(int recipeId, int categoryId)
        {
            return await _context.Recipes.FirstOrDefaultAsync(x => x.Id == recipeId && x.CategoryId == categoryId);
        }

        public async Task<Recipe?> GetAsync(int recipeId)
        {
            return await _context.Recipes.FirstOrDefaultAsync(x => x.Id == recipeId);
        }

        public async Task<IReadOnlyList<Recipe>> GetManyByCategoryAsync(int categoryId)
        {
            return await _context.Recipes.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IReadOnlyList<Recipe>> GetManyAsync()
        {
            return await _context.Recipes.Where(x => x.Id != 0).ToListAsync();
        }

        public async Task CreateAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }
    }
}
