using Foodle.Data.Dtos.Ingredients;
using Foodle.Data.Dtos.Recipes;
using Foodle.Data.Entities;
using Foodle.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Foodle.Controllers
{
    /*
        /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients - GET List 200
        /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId} - GET One 200
        /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/ - POST Create 201
        /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/{igredientId} - PUT/PATCH Modify 200
        /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId} - DELETE modify 200/204
     */

    [ApiController]
    [Route("api/categories/{categoryId}/recipes/{recipeId}/ingredients")]
    public class IngredientsController : ControllerBase
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsController(IRecipesRepository recipesRepository, ICategoriesRepository categoriesRepository, IIngredientsRepository ingredientsRepository)
        {
            _recipesRepository = recipesRepository;
            _categoriesRepository = categoriesRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<IngredientDto>> GetMany(int recipeId)
        {
            var ingredients = await _ingredientsRepository.GetManyAsync(recipeId);

            IEnumerable<IngredientDto> ingredientsDto = ingredients.Select(x => GetIngredientDto(x));

            return ingredientsDto;
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId}
        [HttpGet]
        [Route("{ingredientId}", Name = "GetIngredient")]
        public async Task<ActionResult<IngredientDto>> Get(int recipeId, int ingredientId)
        {
            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            return GetIngredientDto(ingredient);
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/
        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Create(int recipeId, CreateIngredientDto createIngredientDto)
        {
            var ingredient = new Ingredient { Name = createIngredientDto.Name, Description = createIngredientDto.Description, RecipeId = recipeId, Amount = createIngredientDto.Amount, Measurement = createIngredientDto.Measurement };


            await _ingredientsRepository.CreateAsync(ingredient);

            // 201
            return Created("", GetIngredientDto(ingredient));
        }

        [HttpPut]
        [Route("{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> Update(int categoryId, int recipeId, int ingredientId, UpdateIngredientDto updateIngredientDto)
        {

            var recipe = await _recipesRepository.GetAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Name = updateIngredientDto.Name;
            ingredient.Description = updateIngredientDto.Description;
            ingredient.Amount = updateIngredientDto.Amount;
            ingredient.Measurement = updateIngredientDto.Measurement;

            await _ingredientsRepository.UpdateAsync(ingredient);

            return Ok(GetIngredientDto(ingredient));
        }

        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpDelete]
        [Route("{ingredientId}")]
        public async Task<ActionResult> Remove(int ingredientId, int recipeId)
        {
            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            await _ingredientsRepository.DeleteAsync(ingredient);

            //204
            return NoContent();
        }


        public IngredientDto GetIngredientDto(Ingredient ingredient)
        {
            return new IngredientDto(ingredient.Id, ingredient.Name, ingredient.Description, ingredient.Amount, ingredient.Measurement, ingredient.RecipeId);
        }

    }
}
