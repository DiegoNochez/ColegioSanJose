using ColegioSanJose.Data;
using ColegioSanJose.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ColegioSanJose.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportesController(ApplicationDbContext context) => _context = context;

        // /Reportes/Promedios?grado=3°
        public async Task<IActionResult> Promedios(string? grado = null)
        {
            // Dropdown de grados
            var grados = await _context.Alumnos
                .Select(a => a.Grado)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();

            ViewBag.Grados = new SelectList(grados);
            ViewBag.GradoSeleccionado = grado;

            // Base query: expedientes con alumno
            var q = _context.Expedientes
                .Include(e => e.Alumno)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(grado))
            {
                q = q.Where(e => e.Alumno!.Grado == grado);
            }

            var datos = await q
                .GroupBy(e => new { e.AlumnoId, e.Alumno!.Nombre, e.Alumno!.Apellido })
                .Select(g => new PromedioAlumnoVM
                {
                    AlumnoId = g.Key.AlumnoId,
                    Alumno = g.Key.Nombre + " " + g.Key.Apellido,
                    CantidadMaterias = g.Count(),
                    Promedio = Math.Round(g.Average(x => x.NotaFinal), 2)
                })
                .OrderByDescending(x => x.Promedio)
                .ThenBy(x => x.Alumno)
                .ToListAsync();

            // KPI Promedio general del conjunto filtrado (si no hay datos, 0)
            ViewBag.PromedioGeneral = datos.Count > 0
                ? Math.Round(datos.Average(x => x.Promedio), 2)
                : 0m;

            return View(datos);
        }
    }
}
