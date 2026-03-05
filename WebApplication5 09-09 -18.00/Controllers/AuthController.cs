using MegaQr.Api.Data;
using MegaQr.Api.Models;
using MegaQr.Api.Models.DTOs;
using MegaQr.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MegaQr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthService _authService;

    public AuthController(ApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    [Authorize]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        if (_context.Users.Any(u => u.Username == dto.Username))
        {
            return Conflict(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Username already exists!",
                Data = null
            });
        }

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = _authService.HashPassword(dto.Password),
            Role = "Admin"
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new ApiResponseDto<string>
        {
            Success = true,
            Message = "User registered successfully",
            Data = null
        });

    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == dto.Username);
        if (user == null)
        {
            return Unauthorized(new ApiResponseDto<string>
            {
                Success = false,
                Message = "User can't be found!",
                Data = null
            });
        }

        if (user.PasswordHash != _authService.HashPassword(dto.Password))
        {
            return Unauthorized(new ApiResponseDto<string>
            {
                Success = false,
                Message = "Wrong Password!"
            });
        }

        var token = _authService.GenerateJwt(user);

        return Ok(new ApiResponseDto<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful",
            Data = new LoginResponseDto { Token = token }
        });
    }
}
