using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.DTOs.Category;
using Microsoft.EntityFrameworkCore;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesWithProductsAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(new { mensagem = "Nenhuma categoria encontrada" });
            }

            var categoryDtos = categories.Select(c => new CategoryDto(c)).ToList();
            return Ok(new { mensagem = "Categorias encontradas", categorias = categoryDtos });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            try
            {
                var category = _categoryService.GetById(id);
                return Ok(new { mensagem = "Categoria encontrada.", categoria = category });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreateCategoryDto dto)
        {
            try
            {
                var category = _categoryService.Create(dto);
                var categoryDto = new CategoryDto(category);
                return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, new { mensagem = "Categoria criada com sucesso.", categoria = categoryDto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status404NotFound)]
        public IActionResult Update(string id, UpdateCategoryDto dto)
        {
            try
            {
                _categoryService.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            try
            {
                _categoryService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }
        }
    }
}