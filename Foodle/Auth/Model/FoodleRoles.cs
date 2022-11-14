namespace Foodle.Auth.Model
{
    public static class FoodleRoles
    {
        public const string Admin = nameof(Admin);
        public const string ForumUser = nameof(ForumUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, ForumUser };
    }
}
