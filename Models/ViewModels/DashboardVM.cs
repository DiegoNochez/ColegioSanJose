namespace ColegioSanJose.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalAlumnos { get; set; }
        public int TotalMaterias { get; set; }
        public int TotalExpedientes { get; set; }

        public List<string> LabelsTopAlumnos { get; set; } = new();
        public List<double> DataTopAlumnos { get; set; } = new();

        public List<string> LabelsMaterias { get; set; } = new();
        public List<double> DataPromedioMaterias { get; set; } = new();

        public List<string> LabelsGrados { get; set; } = new();
        public List<int> DataAlumnosPorGrado { get; set; } = new();
    }
}
