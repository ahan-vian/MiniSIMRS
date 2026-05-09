using Microsoft.AspNetCore.Mvc;
using MiniSIMRS.DTOs;
using MiniSIMRS.Services;

namespace MiniSIMRS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        if (token == null)
        {
            return Unauthorized(new { message = "Username atau password salah." });
        }

        return Ok(new { Token = token, Message = "Login berhasil." });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        var isSuccess = await _authService.RegisterAsync(dto);

        if (!isSuccess)
        {
            return BadRequest(new { message = "Username sudah digunakan!" });
        }

        return Ok(new { message = "Registrasi berhasil! Silakan login." });
    }
}