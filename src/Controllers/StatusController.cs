using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.Services;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.DTOs.Status;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Status>), StatusCodes.Status200OK)]
        public IActionResult GetStatuses()
        {
            var statuses = _statusService.GetAll();
            return Ok(new { mensagem = "Status encontrados", statuses });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Status), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            try
            {
                var status = _statusService.GetStatusById(id);
                return Ok(new { mensagem = "Status encontrado.", status });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Status não encontrado." });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Status), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateStatusDto dto)
        {
            try
            {
                var status = _statusService.CreateStatus(dto.Name);
                return CreatedAtAction(nameof(GetById), new { id = status.Id }, new { mensagem = "Status criado com sucesso.", status });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status404NotFound)]
        public IActionResult Update(string id, [FromBody] CreateStatusDto dto)
        {
            try
            {
                _statusService.UpdateStatus(id, dto.Name);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Status não encontrado." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Status), StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            try
            {
                _statusService.DeleteStatus(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Status não encontrado." });
            }
        }
    }
}
