using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Models;

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
        [ProducesResponseType(typeof(SelectedProducts), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SelectedProducts), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(SelectedProducts), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SelectedProducts), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SelectedProducts), StatusCodes.Status404NotFound)]
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