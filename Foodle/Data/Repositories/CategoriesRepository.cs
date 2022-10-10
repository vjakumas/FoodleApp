using Foodle.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Foodle.Data.Repositories
{
    public interface ICategoriesRepository
    {
        Task<Category?> GetAsync(int categoryId);
        Task<IReadOnlyList<Category>> GetManyAsync();
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly FoodleDbContext _context;
        public CategoriesRepository(FoodleDbContext foodleDbContext)
        {
            _context = foodleDbContext;
        }

        public async Task<Category?> GetAsync(int topicId)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == topicId);
        }

        public async Task<IReadOnlyList<Category>> GetManyAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}
