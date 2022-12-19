using Foodle.Auth.Model;
using Foodle.Data;
using Foodle.Data.Dtos.Categories;
using Foodle.Data.Dtos.Ingredients;
using Foodle.Data.Dtos.Recipes;
using Foodle.Data.Entities;
using Foodle.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text.Json;


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
    [Route("api/")]
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

        public async Task<IEnumerable<RecipeDto>> GetManyByCategory(int categoryId)
        {
            var recipes = await _recipesRepository.GetManyByCategoryAsync(categoryId);

            IEnumerable<RecipeDto> recipesDto = recipes.Select(x => GetRecipeDto(x));

            return recipesDto;
        }

        //[HttpGet]
        //[Route("recipes")]
        //public async Task<IEnumerable<RecipeDto>> GetManyRecipes()
        //{
        //    var recipes = await _recipesRepository.GetManyAsync();

        //    IEnumerable<RecipeDto> recipesDto = recipes.Select(x => GetRecipeDto(x));

        //    return recipesDto;
        //}

        //api/recipes?pageNumber=1&pageSize=5
        [HttpGet(Name = "GetRecipes")]
        [Route("recipes")]
        public async Task<IEnumerable<RecipeDto>> GetManyPaging([FromQuery] RecipesSearchParameters recipesSearchParameters)
        {
            var recipes = await _recipesRepository.GetManyAsync(recipesSearchParameters);

            //var previousLink = recipes.HasPrevious ?
            //    CreateRecipesResourceUri(recipesSearchParameters, ResourceUriType.PreviousPage) : null;

            //var nextLink = recipes.HasNext ?
            //    CreateRecipesResourceUri(recipesSearchParameters, ResourceUriType.NextPage) : null;

            //var paginationMetadata = new
            //{
            //    totalCount = recipes.TotalCount,
            //    pageSize = recipes.PageSize,
            //    currentPage = recipes.CurrentPage,
            //    totalPages = recipes.TotalPages,
            //    previousLink,
            //    nextLink
            //};


            //Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));


            IEnumerable<RecipeDto> recipesDto = recipes.Select(x => GetRecipeDto(x));

            return recipesDto;
        }

        private string? CreateRecipesResourceUri(RecipesSearchParameters recipesSearchParameters, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetRecipes",
                new
                {
                    pageNumber = recipesSearchParameters.PageNumber - 1,
                    pageSize = recipesSearchParameters.PageSize,
                }),
                ResourceUriType.NextPage => Url.Link("GetRecipes",
                new
                {
                    pageNumber = recipesSearchParameters.PageNumber + 1,
                    pageSize = recipesSearchParameters.PageSize,
                }),
                _ => Url.Link("GetRecipse",
                new
                {
                    pageNumber = recipesSearchParameters.PageNumber,
                    pageSize = recipesSearchParameters.PageSize
                })
            };
        }

        [HttpGet]
        [Route("recipes/{recipeId}")]
        public async Task<ActionResult<RecipeDto>> Get(int recipeId)
        {
            var recipe = await _recipesRepository.GetAsync(recipeId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            return GetRecipeDto(recipe);
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpGet]
        [Route("categories/{categoryId}/recipes/{recipeId}", Name = "GetRecipe")]
        public async Task<ActionResult<RecipeDto>> Get(int recipeId, int categoryId)
        {
            var recipe = await _recipesRepository.GetByCategoryAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            return GetRecipeDto(recipe);
        }


        // /api/v1/categories/{categoryId}/recipes
        [HttpPost]
        [Route("categories/{categoryId}/recipes")]
        [Authorize(Roles = FoodleRoles.ForumUser)]
        public async Task<ActionResult<RecipeDto>> Create(int categoryId, CreateRecipeDto createRecipeDto)
        {
            var recipe = new Recipe
            {
                Name = createRecipeDto.Name,
                ImageURL = createRecipeDto.ImageURL,
                Description = createRecipeDto.Description,
                CategoryId = categoryId, CreationDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
                IsPublic = true
                };

            await _recipesRepository.CreateAsync(recipe);

            // 201
            return Created("", GetRecipeDto(recipe));
        }
        

        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpPut]
        [Route("categories/{categoryId}/recipes/{recipeId}")]
        [Authorize(Roles = FoodleRoles.ForumUser)]
        public async Task<ActionResult<RecipeDto>> Update(int categoryId, int recipeId, UpdateRecipeDto updateRecipeDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);

            // 404
            if (category == null)
            {
                return NotFound();
            }

            var recipe = await _recipesRepository.GetByCategoryAsync(recipeId, categoryId);

            // 404
            if (recipe == null)
            {
                return NotFound();
            }

            if(updateRecipeDto.Name != null)
                recipe.Name = updateRecipeDto.Name;

            if(updateRecipeDto.Description != null)
                recipe.Description = updateRecipeDto.Description;

            if (updateRecipeDto.ImageURL != null)
                recipe.ImageURL = updateRecipeDto.ImageURL;

            recipe.LastUpdateDate = DateTime.UtcNow;

            await _recipesRepository.UpdateAsync(recipe);

            return Ok(GetRecipeDto(recipe));
        }


        // /api/v1/categories/{categoryId}/recipes/{recipeId}
        [HttpDelete]
        [Route("categories/{categoryId}/recipes/{recipeId}")]
        public async Task<ActionResult> Remove(int recipeId, int categoryId)
        {
            var recipe = await _recipesRepository.GetByCategoryAsync(recipeId, categoryId);

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
            return new RecipeDto(recipe.Id, recipe.Name, recipe.ImageURL, recipe.Description, recipe.CreationDate, recipe.LastUpdateDate, recipe.IsPublic, recipe.CategoryId);
        }
    }
}
