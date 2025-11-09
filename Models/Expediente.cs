using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJose.Models
{
    public class Expediente
    {
        public int ExpedienteId { get; set; }

        [Display(Name = "Alumno")]
        [Required(ErrorMessage = "Seleccione un alumno")]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        [Display(Name = "Materia")]
        [Required(ErrorMessage = "Seleccione una materia")]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }

        [Display(Name = "Nota final")]
        [Range(1, 10, ErrorMessage = "La nota debe estar entre {1} y {2}")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal NotaFinal { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }
    }
}
