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

        /api/ingredients - GET List 200
        /api/recipes/{recipeId}/ingredients - Get List 200
     */

    [ApiController]
    [Route("api/")]
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
        [Route("categories/{categoryId}/recipes/{recipeId}/ingredients")]
        public async Task<IEnumerable<IngredientDto>> GetManyByCategoryRecipe(int recipeId)
        {
            var ingredients = await _ingredientsRepository.GetManyAsync(recipeId);

            IEnumerable<IngredientDto> ingredientsDto = ingredients.Select(x => GetIngredientDto(x));

            return ingredientsDto;
        }

        // /api/v1/recipes/{recipeId}/ingredients/{ingredientId}
        [HttpGet]
        [Route("recipes/{recipeId}/ingredients")]
        public async Task<IEnumerable<IngredientDto>> GetManyByRecipe(int recipeId)
        {
            var ingredients = await _ingredientsRepository.GetManyAsync(recipeId);

            IEnumerable<IngredientDto> ingredientsDto = ingredients.Select(x => GetIngredientDto(x));

            return ingredientsDto;
        }

        [HttpGet]
        [Route("ingredients")]
        public async Task<IEnumerable<IngredientDto>> GetMany()
        {
            var ingredients = await _ingredientsRepository.GetManyAsync();

            IEnumerable<IngredientDto> ingredientsDto = ingredients.Select(x => GetIngredientDto(x));

            return ingredientsDto;
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId}
        [HttpGet]
        [Route("categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> GetByCategoryRecipe(int recipeId, int ingredientId)
        {
            var ingredient = await _ingredientsRepository.GetAsync(recipeId, ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            return GetIngredientDto(ingredient);
        }

        // /api/recipes/{recipeId}/ingredients/{ingredientId}
        [HttpGet]
        [Route("recipes/{recipeId}/ingredients/{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> GetByRecipe(int ingredientId)
        {
            var ingredient = await _ingredientsRepository.GetIngredientAsync(ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            return GetIngredientDto(ingredient);
        }

        // /api/ingredients/{ingredientId}
        [HttpGet]
        [Route("ingredients/{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> Get(int ingredientId)
        {
            var ingredient = await _ingredientsRepository.GetIngredientAsync(ingredientId);

            // 404
            if (ingredient == null)
            {
                return NotFound();
            }

            return GetIngredientDto(ingredient);
        }

        // /api/v1/categories/{categoryId}/recipes/{recipeId}/ingredients/
        [HttpPost]
        [Route("categories/{categoryId}/recipes/{recipeId}/ingredients")]
        public async Task<ActionResult<IngredientDto>> Create(int recipeId, CreateIngredientDto createIngredientDto)
        {
            var ingredient = new Ingredient { Name = createIngredientDto.Name, Description = createIngredientDto.Description, RecipeId = recipeId, Amount = createIngredientDto.Amount, Measurement = createIngredientDto.Measurement };


            await _ingredientsRepository.CreateAsync(ingredient);

            // 201
            return Created("", GetIngredientDto(ingredient));
        }

        [HttpPut]
        [Route("categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId}")]
        public async Task<ActionResult<IngredientDto>> Update(int categoryId, int recipeId, int ingredientId, UpdateIngredientDto updateIngredientDto)
        {

            var recipe = await _recipesRepository.GetByCategoryAsync(recipeId, categoryId);

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

            if(updateIngredientDto.Name != null)
                ingredient.Name = updateIngredientDto.Name;

            if(updateIngredientDto.Description != null)
                ingredient.Description = updateIngredientDto.Description;
            
            if(updateIngredientDto.Amount != 0)
                ingredient.Amount = updateIngredientDto.Amount;

            if(updateIngredientDto.Measurement != null)
                ingredient.Measurement = updateIngredientDto.Measurement;

            await _ingredientsRepository.UpdateAsync(ingredient);

            return Ok(GetIngredientDto(ingredient));
        }

        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpDelete]
        [Route("categories/{categoryId}/recipes/{recipeId}/ingredients/{ingredientId}")]
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

        [HttpGet]
        [Route("recipe/{recipeId}/ingredients")]
        public async Task<IEnumerable<IngredientDto>> GetManyIngredientsByRecipe()
        {
            var ingredients = await _ingredientsRepository.GetManyAsync();

            IEnumerable<IngredientDto> ingredientsDto = ingredients.Select(x => GetIngredientDto(x));

            return ingredientsDto;
        }


        public IngredientDto GetIngredientDto(Ingredient ingredient)
        {
            return new IngredientDto(ingredient.Id, ingredient.Name, ingredient.Description, ingredient.Amount, ingredient.Measurement, ingredient.RecipeId);
        }

    }
}
