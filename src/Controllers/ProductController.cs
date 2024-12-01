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
            return Ok(new { mensagem = "Produtos encontrados", produtos = products });
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetByCategory(string category)
        {
            var products = _productService.GetByCategory(category);
            if (!products.Any())
            {
                return NotFound(new { mensagem = "Nenhum produto encontrado para esta categoria." });
            }
            return Ok(new { mensagem = "Produtos encontrados", produtos = products });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create(CreateProductDto dto)
        {
            try
            {
                var product = _productService.Create(dto);
                var productDto = new ProductDto(product);
                return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, new { mensagem = "Produto criado com sucesso.", produto = productDto });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = "Erro ao criar produto", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(string id, [FromBody] UpdateProductDto dto) 
        {
            try
            {
                _productService.Update(id, dto); 
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
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
