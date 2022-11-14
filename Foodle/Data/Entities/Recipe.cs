using Foodle.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace Foodle.Data.Entities
{
    public class Recipe : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsPublic { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public string UserId { get; set; }
        public FoodleRestUser User { get; set; }
    }
}
