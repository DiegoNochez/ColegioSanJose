using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Display(Name = "Materia")]
        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string NombreMateria { get; set; } = string.Empty;

        [Display(Name = "Docente")]
        [Required(ErrorMessage = "El docente es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Docente { get; set; } = string.Empty;

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}
