using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.DTOs;
using myBURGUERMANIA_API.Services;

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
        [ProducesResponseType(typeof(OrderService), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] CreateOrderDto createOrderDto)
        {
            var order = _orderService.Create(createOrderDto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderService), StatusCodes.Status200OK)]
        public IActionResult GetById(string id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(OrderService), StatusCodes.Status200OK)]
        public IActionResult Update(string id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            var order = _orderService.Update(id, updateOrderDto);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}