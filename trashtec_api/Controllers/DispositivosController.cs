using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trashtec_api.Data;
using trashtec_api.dTOs.Dispositivos;
using trashtec_api.dTOs.Usuarios;
using trashtec_api.Models;

namespace trashtec_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DispositivosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Dispositivos (Obtener todos los dispositivos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispositivoModel>>> GetDispositivos()
        {
            return await _context.Dispositivos.ToListAsync();
        }

        // GET: api/Dispositivos/{id} (Obtener dispositivo por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<DispositivoModel>> GetDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivos.FindAsync(id);

            if (dispositivo == null)
            {
                return NotFound(new { mensaje = "Dispositivo no encontrado" });
            }

            return dispositivo;
        }
        /*
        [HttpPost("agregar")]
        public IActionResult AgregarDispositivo([FromBody] DispositivoModel dispositivo)
        {
            if (dispositivo == null)
                return BadRequest("Datos inválidos");

          

            _context.Dispositivos.Add(dispositivo);
            _context.SaveChanges();
            return Ok(new { mensaje = "Dispositivo agregado correctamente", dispositivo.IdDispositivo });
        }
        */
        [HttpPost("agregar")]
        public IActionResult AgregarDispotivo([FromBody] AgregarDmodel agregardispostivo)
        {
            if (agregardispostivo == null)
                return BadRequest("Datos inválidos");

            // Crear una instancia de Usuario basada en UsuarioRegistro
            var Dispositivo = new DispositivoModel
            {
                // Asigna los valores del modelo UsuarioRegistro al modelo Usuario
                Nombre =agregardispostivo.Nombre,
                Tipo = agregardispostivo.Tipo,

            };


            _context.Dispositivos.Add(Dispositivo);
            _context.SaveChanges();

            // Retornar una respuesta con el mensaje y el Id del nuevo usuario
            return Ok(new { mensaje = "Dispositivo agregado correctamente", Dispositivo.IdDispositivo });
        }

        // PUT: api/Dispositivos/{id} (Actualizar dispositivo)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivo(int id, DispositivoModel dispositivo)
        {
            if (id != dispositivo.IdDispositivo)
            {
                return BadRequest(new { mensaje = "El ID del dispositivo no coincide con el de la URL" });
            }

            _context.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dispositivos.Any(e => e.IdDispositivo == id))
                {
                    return NotFound(new { mensaje = "Dispositivo no encontrado" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Dispositivos/{id} (Eliminar dispositivo)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivo(int id)
        {
            var dispositivo = await _context.Dispositivos.FindAsync(id);
            if (dispositivo == null)
            {
                return NotFound(new { mensaje = "Dispositivo no encontrado" });
            }

            _context.Dispositivos.Remove(dispositivo);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Dispositivo eliminado exitosamente" });
        }
    }
}


