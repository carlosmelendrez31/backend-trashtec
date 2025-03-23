using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trashtec_api.Data;
using trashtec_api.dTOs.Usuarios;
using trashtec_api.Models;

namespace trashtec_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios (Obtener todos los usuarios)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosModel>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/{id} (Obtener usuario por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosModel>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            return usuario;
        }
        [HttpPost("agregar")]
        public IActionResult AgregarUsuario([FromBody] UsuarioRegistro usuarioRegistro)
        {
            if (usuarioRegistro == null)
                return BadRequest("Datos inválidos");

            // Crear una instancia de Usuario basada en UsuarioRegistro
            var usuario = new UsuariosModel
            {
                // Asigna los valores del modelo UsuarioRegistro al modelo Usuario
                nombreusuario = usuarioRegistro.nombreusuario,
                email = usuarioRegistro.email,
                contrasena = usuarioRegistro.contrasena, 
            };

            
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            // Retornar una respuesta con el mensaje y el Id del nuevo usuario
            return Ok(new { mensaje = "Usuario agregado correctamente", usuario.IdUsuario });
        }


        // PUT: api/Usuarios/{id} (Actualizar usuario)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioRegistro usuarioRegistro)
        {
            // Buscar el usuario existente en la base de datos usando el ID
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            
            usuario.nombreusuario = usuarioRegistro.nombreusuario;
            usuario.email = usuarioRegistro.email;
            usuario.contrasena = usuarioRegistro.contrasena; 

            
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.IdUsuario == id))
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna NoContent si la actualización fue exitosa
        }

        // DELETE: api/Usuarios/{id} (Eliminar usuario)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario eliminado exitosamente" });
        }
    }
}

