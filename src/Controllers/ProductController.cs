using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.DTOs.Product;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll(); 
            return Ok(new { message = "Produtos encontrados", data = products });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id) 
        {
            var product = _productService.GetById(id); 
            if (product == null)
                return NotFound(new { message = "Produto não encontrado." });
            return Ok(new { message = "Produto encontrado.", data = product });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create(CreateProductDto dto)
        {
            try
            {
                var product = _productService.Create(dto);
                var productDto = new ProductDto(product);
                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, new { message = "Produto criado com sucesso.", data = productDto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Erro ao criar produto", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] UpdateProductDto dto) 
        {
            try
            {
                _productService.Update(id, dto); 
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Produto não encontrado." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) 
        {
            try
            {
                _productService.Delete(id); 
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Produto não encontrado." });
            }
        }
    }
}
