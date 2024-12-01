using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.DTOs;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Order;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = _orderService.Create(createOrderDto);
            if (order == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado" });
            }
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound(new { mensagem = "Pedido não encontrado" });
            }
            return Ok(order);
        }

        public class UpdateStatusDto
        {
            public int Status { get; set; }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status404NotFound)]
        public IActionResult UpdateStatus(string id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = _orderService.UpdateStatus(id, updateStatusDto.Status);
            if (order == null)
            {
                return NotFound(new { mensagem = "Pedido não encontrado" });
            }
            return Ok(order);
        }

        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Cancel(string id)
        {
            var order = _orderService.Cancel(id);
            if (order == null)
            {
                return NotFound(new { mensagem = "Pedido não encontrado" });
            }
            return Ok(order);
        }

    }
}
