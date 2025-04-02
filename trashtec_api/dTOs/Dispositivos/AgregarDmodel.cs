using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace trashtec_api.dTOs.Dispositivos
{
    public class AgregarDmodel
    {

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Tipo")]
        public string Tipo { get; set; }
    }
}
