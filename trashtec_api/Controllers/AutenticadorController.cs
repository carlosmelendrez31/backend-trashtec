using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AutenticadorController : ControllerBase
{
    private readonly AuthServices _authService;

    public AutenticadorController(AuthServices authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.ValidateUserAndGenerateTokenAsync(request.email, request.contrasena);

        if (token == null)
            return Unauthorized("Credenciales incorrectas");

        return Ok(new { token });
    }
}

public class LoginRequest
{
    public string? email { get; set; }
    public string? contrasena { get; set; }
}
