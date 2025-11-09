using ColegioSanJose.Data;
using ColegioSanJose.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColegioSanJose.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardVM();

            // KPIs
            vm.TotalAlumnos = await _context.Alumnos.CountAsync();
            vm.TotalMaterias = await _context.Materias.CountAsync();
            vm.TotalExpedientes = await _context.Expedientes.CountAsync();

            // Top 5 alumnos por promedio
            var top = await _context.Expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => new { e.AlumnoId, e.Alumno!.Nombre, e.Alumno!.Apellido })
                .Select(g => new
                {
                    Nombre = g.Key.Nombre + " " + g.Key.Apellido,
                    Promedio = Math.Round(g.Average(x => (double)x.NotaFinal), 2)
                })
                .OrderByDescending(x => x.Promedio)
                .Take(5)
                .ToListAsync();

            vm.LabelsTopAlumnos = top.Select(x => x.Nombre).ToList();
            vm.DataTopAlumnos = top.Select(x => x.Promedio).ToList();

            // Promedio por materia
            var promMat = await _context.Expedientes
                .Include(e => e.Materia)
                .GroupBy(e => new { e.MateriaId, e.Materia!.NombreMateria })
                .Select(g => new
                {
                    Materia = g.Key.NombreMateria,
                    Promedio = Math.Round(g.Average(x => (double)x.NotaFinal), 2)
                })
                .OrderByDescending(x => x.Promedio)
                .ToListAsync();

            vm.LabelsMaterias = promMat.Select(x => x.Materia).ToList();
            vm.DataPromedioMaterias = promMat.Select(x => x.Promedio).ToList();

            // Alumnos por grado
            var porGrado = await _context.Alumnos
                .GroupBy(a => a.Grado)
                .Select(g => new { Grado = g.Key, Cant = g.Count() })
                .OrderBy(x => x.Grado)
                .ToListAsync();

            vm.LabelsGrados = porGrado.Select(x => x.Grado).ToList();
            vm.DataAlumnosPorGrado = porGrado.Select(x => x.Cant).ToList();

            return View(vm);
        }
    }
}
