using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.SelectedProducts; // Adicionar importação para DTO

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SelectedProductsController : ControllerBase
    {
        private readonly SelectedProductsService _selectedProductsService;

        public SelectedProductsController(SelectedProductsService selectedProductsService)
        {
            _selectedProductsService = selectedProductsService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SelectedProductsDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SelectedProductsDto), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] List<string> productIds)
        {
            try
            {
                var selectedProducts = _selectedProductsService.Create(productIds);
                return CreatedAtAction(nameof(GetById), new { id = selectedProducts.Id }, selectedProducts);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                _selectedProductsService.Remove(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SelectedProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SelectedProductsDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SelectedProductsDto), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            try
            {
                var selectedProducts = _selectedProductsService.GetById(id);
                if (selectedProducts == null)
                {
                    return NotFound();
                }
                return Ok(selectedProducts);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}