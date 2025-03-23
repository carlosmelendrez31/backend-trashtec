using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class AuthServices
{
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly string _supabaseConnectionString;

    public AuthServices(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"];
        _jwtIssuer = configuration["Jwt:Issuer"];
        _jwtAudience = configuration["Jwt:Audience"];
        _supabaseConnectionString = configuration.GetConnectionString("DefaultConnection");

    }

    public async Task<string> ValidateUserAndGenerateTokenAsync(string email, string contrasena)
    {
        // Conectar a la base de datos de Supabase
        await using var connection = new NpgsqlConnection(_supabaseConnectionString);
        await connection.OpenAsync();

        // Buscar el usuario con email y contraseña
        string query = "SELECT \"idUsuario\", \"nombreusuario\" FROM \"Usuarios\" WHERE  \"email\" = @Email AND contrasena = crypt(@Password, contrasena)";
        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("Password", contrasena);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
            return null; // Usuario no encontrado

        var userId = reader["idUsuario"].ToString();
        var nombreUsuario = reader["nombreusuario"].ToString();

        return GenerateToken(email, userId, nombreUsuario);  // Pasamos nombreUsuario a la función de generación del token
    }

    private string GenerateToken(string email, string userId, string nombreusuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Name, nombreusuario),  // Aquí agregamos el nombreUsuario
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
