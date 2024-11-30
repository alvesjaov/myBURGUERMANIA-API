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
        public IActionResult GetUser(string id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(string id, [FromBody] UpdateUserDto updateUserDTO)
        {
            if (!UserService.IsValidUser(updateUserDTO))
            {
                return BadRequest(new { message = "Dados inválidos." });
            }

            try
            {
                _userService.UpdateUser(id, updateUserDTO);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
        }
    }
}
