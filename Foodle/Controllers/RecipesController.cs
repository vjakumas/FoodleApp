using Foodle.Data.Dtos.Categories;
using Foodle.Data.Dtos.Recipes;
using Foodle.Data.Entities;
using Foodle.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;


/*
    /api/v1/categories/{categoryId}/recipes - GET List 200
    /api/v1/categories/{categoryId}/recipes/{recipeId} - GET One 200
    /api/v1/categories/{categoryId}/recipes - POST Create 201
    /api/v1/categories/{categoryId}/recipes/{recipeId} - PUT/PATCH Modify 200
    /api/v1/categories/{categoryId}/recipes/{recipeId} - DELETE modify 200/204
 */

namespace Foodle.Controllers
{
    [ApiController]
    [Route("api/categories/{categoryId}/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public RecipesController(IRecipesRepository recipesRepository, ICategoriesRepository categoriesRepository)
        {
            _recipesRepository = recipesRepository;
            _categoriesRepository = categoriesRepository;
        }

        // /api/v1/categories/{categoryId}/recipes
        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> GetMany(int categoryId)
        {
            var recipes = await _recipesRepository.GetManyAsync(categoryId);

            IEnumerable<RecipeDto> recipesDto = recipes.Select(x => GetRecipeDto(x));

            return recipesDto;
        }

        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpGet]
        [Route("{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> Get(int recipeId, int categoryId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            return GetRecipeDto(recipe);
        }


        // /api/v1/categories/{categoryId}/recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Create(int categoryId, CreateRecipeDto createRecipeDto)
        {
            var recipe = new Recipe { Name = createRecipeDto.Name, Description = createRecipeDto.Description, CategoryId = categoryId, CreationDate = DateTime.UtcNow, LastUpdateDate = DateTime.UtcNow, IsPublic = true };

            await _recipesRepository.CreateAsync(recipe);

            // 201
            return Created("", GetRecipeDto(recipe));
        }
        

        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpPut]
        [Route("{recipeId}")]
        public async Task<ActionResult<RecipeDto>> Update(int categoryId, int recipeId, UpdateRecipeDto updateCategoryDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);

            // 404
            if (category == null)
            {
                return NotFound();
            }

            var recipe = await _recipesRepository.GetAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Name = updateCategoryDto.Name;
            recipe.Description = updateCategoryDto.Description;
            recipe.LastUpdateDate = DateTime.UtcNow;

            await _recipesRepository.UpdateAsync(recipe);

            return Ok(GetRecipeDto(recipe));
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpDelete]
        [Route("{recipeId}")]
        public async Task<ActionResult> Remove(int recipeId, int categoryId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            await _recipesRepository.DeleteAsync(recipe);

            //204
            return NoContent();


        }


        public RecipeDto GetRecipeDto(Recipe recipe)
        {
            return new RecipeDto(recipe.Id, recipe.Name, recipe.Description, recipe.CreationDate, recipe.LastUpdateDate, recipe.IsPublic, recipe.CategoryId);
        }
    }
}
