using AspNetWebApiStarter.Libs;
using AspNetWebApiStarter.Models.DTOs.Auth;
using AspNetWebApiStarter.Models.DTOs.User;
using AspNetWebApiStarter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetWebApiStarter.Controllers
{
  [Route("api/[controller]")]
  [AllowAnonymous]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly Utils _utils;
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public AuthController(AuthService authService, UserService userService, Utils utils)
    {
      _utils = utils;
      _authService = authService;
      _userService = userService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
      try
      {
        var user = await _authService.Login(loginDto);

        if (user == null)
        {
          return BadRequest(new { message = "Invalid credentials" });
        }

        var token = _utils.GenerateJwtToken(user);

        return Ok(new { user, token });
      }
      catch (Exception ex)
      {
        return StatusCode(
          StatusCodes.Status500InternalServerError,
          new { message = "Something went wrong" }
        );
      }
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDto)
    {
      try
      {
        var user = await _authService.Register(createUserDto);

        if (user == null)
        {
          return BadRequest(new { message = "Invalid credentials" });
        }

        var token = _utils.GenerateJwtToken(user);

        return Created("", new { user, token });
      }
      catch
      {
        return StatusCode(
          StatusCodes.Status500InternalServerError,
          new { message = "Something went wrong" }
        );
      }
    }

    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Profile()
    {
      try
      {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
          return Unauthorized(new { message = "Unauthorized" });
        }

        var user = await _userService.FindById(int.Parse(userId));

        if (user == null)
        {
          return NotFound(new { message = "User not found" });
        }

        return Ok(user);
      }
      catch
      {
        return StatusCode(
          StatusCodes.Status500InternalServerError,
          new { message = "Something went wrong" }
        );
      }
    }
  }
}

