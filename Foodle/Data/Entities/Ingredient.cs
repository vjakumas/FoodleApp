using Foodle.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace Foodle.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Measurement { get; set; }
        public int RecipeId { get; set; }
    }
}
