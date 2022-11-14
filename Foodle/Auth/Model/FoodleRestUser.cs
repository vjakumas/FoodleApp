using Microsoft.AspNetCore.Identity;

namespace Foodle.Auth.Model
{
    public class FoodleRestUser : IdentityUser
    {
        [PersonalData]
        public string? UserData { get; set; }
    }
}
