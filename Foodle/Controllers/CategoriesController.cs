using Foodle.Data.Dtos.Categories;
using Foodle.Data.Entities;
using Foodle.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;


/*
/api/v1/categories - GET List 200
/api/v1/categories/{id} - GET One 200
/api/v1/categories - POST Create 201
/api/v1/categories/{id} - PUT/PATCH Modify 200
/api/v1/categories/{id} - DELETE modify 200/204
*/


namespace Foodle.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // /api/v1/categories
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetMany()
        {
            var categories = await _categoriesRepository.GetManyAsync();

            IEnumerable<CategoryDto> categoriesDto = categories.Select(x => GetCategoryDto(x));

            return categoriesDto;
        }


        // /api/v1/categories/{id}
        [HttpGet]
        [Route("{categoryId}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);

            // 404
            if(category == null)
            {
                return NotFound();
            }

            return GetCategoryDto(category);
        }


        // /api/v1/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto createCategryDto)
        {
            var category = new Category { Name = createCategryDto.Name, Description = createCategryDto.Description, CreationDate = DateTime.UtcNow, LastUpdateDate = DateTime.UtcNow, IsEnabled = true };

            await _categoriesRepository.CreateAsync(category);

            // 201
            return Created("", GetCategoryDto(category));
        }


        // /api/v1/categories/{id}
        [HttpPut]
        [Route("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> Update(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);

            // 404
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;
            category.LastUpdateDate = DateTime.UtcNow;

            await _categoriesRepository.UpdateAsync(category);

            return Ok(GetCategoryDto(category));
        }

        // /api/v1/categories/{id}
        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<ActionResult> Remove(int categoryId)
        {
            var category = await _categoriesRepository.GetAsync(categoryId);

            // 404
            if (category == null)
            {
                return NotFound();
            }

            await _categoriesRepository.DeleteAsync(category);

            //204
            return NoContent();


        }

        public CategoryDto GetCategoryDto(Category category)
        {
            return new CategoryDto(category.Id, category.Name, category.Description, category.CreationDate, category.LastUpdateDate, category.IsEnabled);
        }
    }
}
