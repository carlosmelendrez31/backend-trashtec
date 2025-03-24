using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trashtec_api.dTOs.Usuarios
{
    public class UsuarioRegistro
    {
        [Required]
        [Column("nombreusuario")]
        public string nombreusuario { get; set; }

        [Required]
        [Column("email")]
        public string email { get; set; }

        private string? _contrasena;

        [Required]
        [Column("contrasena")]
        public string contrasena
        {
            get => _contrasena;
            set => _contrasena = BCrypt.Net.BCrypt.HashPassword(value); // Encripta la contraseña al asignarla
        }
        public bool VerificarContrasena(string contrasenaAComparar)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenaAComparar, _contrasena);
        }

    }
}
