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

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDTO)
        {
            if (!UserService.IsValidUser(createUserDTO))
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                var user = _userService.CreateUser(createUserDTO);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDTO)
        {
            if (!UserService.IsValidUser(updateUserDTO))
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            if (!_userService.UpdateUser(id, updateUserDTO))
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int id)
        {
            if (!_userService.DeleteUser(id))
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return NoContent();
        }
    }
}
