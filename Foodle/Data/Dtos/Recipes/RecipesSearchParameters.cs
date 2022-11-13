namespace Foodle.Data.Dtos.Recipes
{
    public class RecipesSearchParameters
    {
        // api/recipes
        private int _pageSize = 2;
        private const int MaxPageSize = 50;

        public int PageNumber { get; init; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
