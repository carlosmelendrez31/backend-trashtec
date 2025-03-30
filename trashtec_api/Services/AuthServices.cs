using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

    public async Task<object> ValidateUserAndGenerateTokenAsync(string email, string contrasena)
    {
        await using var connection = new NpgsqlConnection(_supabaseConnectionString);
        await connection.OpenAsync();

        // 1️⃣ Obtener usuario con su contraseña
        string query = "SELECT \"idUsuario\", \"nombreusuario\", \"contrasena\" FROM \"Usuarios\" WHERE \"email\" = @Email";
        await using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("Email", email);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) // 🔹 Asegurar el uso de await
        {
            Console.WriteLine("❌ Usuario no encontrado.");
            return null;
        }

        var userId = reader["idUsuario"].ToString();
        var nombreUsuario = reader["nombreusuario"].ToString();
        var hashedPassword = reader["contrasena"].ToString();

        reader.Close(); // 🔹 Cierra el primer reader ANTES de ejecutar otra consulta

        var usuario = new UsuariosModel { contrasena = hashedPassword };
        if (!usuario.VerificarContrasena(contrasena))
        {
            Console.WriteLine("❌ Contraseña incorrecta.");
            return null;
        }

        Console.WriteLine("✅ Usuario autenticado correctamente.");

        // 2️⃣ Obtener los dispositivos asociados al usuario
        List<string> dispositivos = new List<string>();
        string deviceQuery = "SELECT \"idDispositivo\" FROM \"Dispositivos\" WHERE \"idUsuario\" = @UserId";
        await using var deviceCmd = new NpgsqlCommand(deviceQuery, connection);
        deviceCmd.Parameters.AddWithValue("UserId", Convert.ToInt32(userId));

        await using var deviceReader = await deviceCmd.ExecuteReaderAsync();
        while (await deviceReader.ReadAsync()) // 🔹 Usar await aquí
        {
            dispositivos.Add(deviceReader["idDispositivo"].ToString());
        }

        // 3️⃣ Generar token
        string token = GenerateToken(email, userId, nombreUsuario, dispositivos);

        // 4️⃣ Devolver el token y la lista de dispositivos
        return new
        {
            Token = token,
            Dispositivos = dispositivos
        };
    }


    private string GenerateToken(string email, string userId, string nombreUsuario, List<string> dispositivos)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim("id", userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, nombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // 🔥 Agregar cada dispositivo como un claim
        foreach (var dispositivo in dispositivos)
        {
            claims.Add(new Claim("idDispositivo", dispositivo));
        }

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

