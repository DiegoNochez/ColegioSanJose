using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioSanJose.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo {1} caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo {1} caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [NotInFuture(ErrorMessage = "La fecha no puede ser futura")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Grado")]
        [Required(ErrorMessage = "El grado es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo {1} caracteres")]
        public string Grado { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}
