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
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(new { mensagem = "Produtos encontrados", produtos = products });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(new { mensagem = "Produto encontrado.", produto = product });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Produto não encontrado." });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public IActionResult Create(CreateProductDto dto)
        {
            try
            {
                var productDto = _productService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, new { mensagem = "Produto criado com sucesso.", produto = productDto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public IActionResult Update(string id, UpdateProductDto dto)
        {
            try
            {
                _productService.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = "Produto não encontrado." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Produto não encontrado." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
