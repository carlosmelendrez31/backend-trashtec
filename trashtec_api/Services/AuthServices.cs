using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using trashtec_api.Models;

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
        await using var connection = new NpgsqlConnection(_supabaseConnectionString);
        await connection.OpenAsync();

        string query = "SELECT \"idUsuario\", \"nombreusuario\", \"contrasena\" FROM \"Usuarios\" WHERE \"email\" = @Email";
        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("Email", email);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
        {
            Console.WriteLine("❌ Usuario no encontrado.");
            return null;
        }

        var userId = reader["idUsuario"].ToString();
        var nombreUsuario = reader["nombreusuario"].ToString();
        var hashedPassword = reader["contrasena"].ToString(); // Contraseña cifrada desde la base de datos

        // 🔹 Mostrar la contraseña encriptada en la consola
        Console.WriteLine($"🔹 Hashed Password from DB: {hashedPassword}");

        // Crear un objeto de UsuarioModel y comparar la contraseña ingresada
        var usuario = new UsuariosModel { contrasena = hashedPassword };
        bool isPasswordValid = usuario.VerificarContrasena(contrasena);

        if (!isPasswordValid)
        {
            Console.WriteLine("❌ Contraseña incorrecta.");
            return null;
        }

        Console.WriteLine("✅ Usuario autenticado correctamente.");
        return GenerateToken(email, userId, nombreUsuario);
    }


    private string GenerateToken(string email, string userId, string nombreUsuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userId), // Esto es válido, pero Angular no lo está extrayendo
        new Claim("id", userId), // 🔥 Agregamos el id con un nombre explícito
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Name, nombreUsuario),
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
