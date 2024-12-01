using Microsoft.AspNetCore.Mvc;
using myBURGUERMANIA_API.DTOs.User;
using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Services;
using System.Collections.Generic;
using System;

namespace myBURGUERMANIA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private const string DataInvalidMessage = "Dados inválidos.";
        private const string UserNotFoundMessage = "Usuário não encontrado.";

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(User), StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDTO)
        {
            if (!UserService.IsValidUser(createUserDTO))
            {
                return BadRequest(new { mensagem = DataInvalidMessage });
            }

            try
            {
                var user = _userService.CreateUser(createUserDTO);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(User), StatusCodes.Status404NotFound)]
        public IActionResult GetUserByCpf(string cpf)
        {
            if (cpf.Contains(".") || cpf.Contains("-") || !cpf.All(char.IsDigit))
            {
                return BadRequest(new { mensagem = "O CPF deve conter apenas números." });
            }

            try
            {
                var user = _userService.GetUserByCpf(cpf);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = UserNotFoundMessage });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = UserNotFoundMessage });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(User), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(User), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(string id, [FromBody] UpdateUserDto updateUserDTO)
        {
            if (!UserService.IsValidUser(updateUserDTO))
            {
                return BadRequest(new { mensagem = DataInvalidMessage });
            }

            try
            {
                _userService.UpdateUser(id, updateUserDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = UserNotFoundMessage });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(User), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = UserNotFoundMessage });
            }
        }
    }
}
