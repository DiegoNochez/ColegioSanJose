namespace ColegioSanJose.Models.ViewModels
{
    public class PromedioAlumnoVM
    {
        public int AlumnoId { get; set; }
        public string Alumno { get; set; } = string.Empty;
        public int CantidadMaterias { get; set; }
        public decimal Promedio { get; set; }
    }
}
