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

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public IActionResult GetByCategory(string categoryId)
        {
            var products = _productService.GetByCategory(categoryId);
            return Ok(new { mensagem = "Produtos encontrados", produtos = products });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        public IActionResult Create(CreateProductDto dto)
        {
            var productDto = _productService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, new { mensagem = "Produto criado com sucesso.", produto = productDto });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status404NotFound)]
        public IActionResult Update(string id, UpdateProductDto dto)
        {
            try
            {
                _productService.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Produto não encontrado." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        }
    }
}
