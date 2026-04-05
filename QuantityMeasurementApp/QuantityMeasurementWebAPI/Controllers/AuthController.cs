using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementRepositoryLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.DTO;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwt;

    public AuthController(IUserRepository repository, JwtService jwt)
    {
        _repository = repository;
        _jwt = jwt;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var existing = await _repository.GetByEmailAsync(request.Email);
        if (existing != null) return BadRequest("Email already exists");

        var user = new UserEntity
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(request.Password),
            Role = string.IsNullOrWhiteSpace(request.Role) ? "user" : request.Role
        };

        await _repository.AddUserAsync(user);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(user.Id, user.Email, user.Role);
        return Ok(new { Token = token });
    }
}

