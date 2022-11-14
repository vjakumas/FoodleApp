using Microsoft.EntityFrameworkCore;
using Foodle.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Foodle.Auth.Model;

namespace Foodle.Data
{
    public class FoodleDbContext : IdentityDbContext<FoodleRestUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FoodleDb");
        }


    }
}
