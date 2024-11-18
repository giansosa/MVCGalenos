using System.ComponentModel.DataAnnotations;

namespace MVCGalenos.Models
{
    public class TurnoViewModel
    {
        [Required]
        public int IdCita { get; set; }
        [Required]
        public int IdAfiliado { get; set; }
        [Required]
        public int IdPrestador { get; set; }
        [Required]
        public Especialidad Especialidad { get; set; }
        public DateTime FechaCita { get; set; }
    }
}


