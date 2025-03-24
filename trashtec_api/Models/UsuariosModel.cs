using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

namespace trashtec_api.Models
{
    [Table("Usuarios")]
    public class UsuariosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("nombreusuario")]
        public string nombreusuario { get; set; }

        [Required]
        [Column("email")]
        public string email { get; set; }

        private string _contrasena;

        [Required]
        [Column("contrasena")]
        public string contrasena
        {
            get => _contrasena;
            set => _contrasena = value; // Ya no se cifra aquí
        }

        [Column("dispositivoid")]
        public int? dispositivoId { get; set; }  // 🔹 FK opcional

        // Método para verificar contraseñas
        public bool VerificarContrasena(string contrasenaAComparar)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenaAComparar, _contrasena);
        }
    }
}
