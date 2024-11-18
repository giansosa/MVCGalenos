using System.ComponentModel.DataAnnotations;

namespace MVCGalenos.Models
{
    public class Laboratorio
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Especialidad")]
        public Especialidad Especialidad { get; set; }

        [Required]
        [Display(Name = "Prestador Médico")]
        public string PrestadorMedico { get; set; }

        [Required]
        [Display(Name = "Archivo de Estudio")]
        public IFormFile ArchivoEstudio { get; set; }
    }
}
