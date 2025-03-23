using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trashtec_api.Models
{
    [Table("Dispositivos")]
    public class DispositivoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Column("idDispositivo")]
        public int IdDispositivo { get; set; }

        [Required]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Tipo")]
        public string Tipo { get; set; }

        [Required]
        [Column("Llenado")]
        public int Llenado { get; set; }

      
    }
}


