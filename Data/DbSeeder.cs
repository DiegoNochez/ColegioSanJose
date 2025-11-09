using ColegioSanJose.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioSanJose.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext ctx)
        {
            await ctx.Database.MigrateAsync();

            // Si ya hay alumnos, no hacer nada
            if (await ctx.Alumnos.AnyAsync())
                return;

            // ----- Alumnos (1° a 9° grado) -----
            var alumnos = new List<Alumno>
            {
                new Alumno { Nombre = "Sofía",   Apellido = "Hernández", Grado = "1°", FechaNacimiento = new DateTime(2016, 5, 12) },
                new Alumno { Nombre = "Diego",   Apellido = "Castro",    Grado = "2°", FechaNacimiento = new DateTime(2015, 3, 22) },
                new Alumno { Nombre = "Camila",  Apellido = "Martínez",  Grado = "3°", FechaNacimiento = new DateTime(2014, 7,  8) },
                new Alumno { Nombre = "Mateo",   Apellido = "Ramírez",   Grado = "4°", FechaNacimiento = new DateTime(2013, 1, 17) },
                new Alumno { Nombre = "Valeria", Apellido = "López",     Grado = "5°", FechaNacimiento = new DateTime(2012, 11, 29) },
                new Alumno { Nombre = "Andrés",  Apellido = "González",  Grado = "6°", FechaNacimiento = new DateTime(2011, 4, 14) },
                new Alumno { Nombre = "Lucía",   Apellido = "Flores",    Grado = "7°", FechaNacimiento = new DateTime(2010, 8, 19) },
                new Alumno { Nombre = "Sebastián",Apellido = "Díaz",     Grado = "8°", FechaNacimiento = new DateTime(2009, 9,  5) },
                new Alumno { Nombre = "Mariana", Apellido = "Aguilar",   Grado = "9°", FechaNacimiento = new DateTime(2008, 6, 30) }
            };
            ctx.Alumnos.AddRange(alumnos);
            await ctx.SaveChangesAsync();

            // ----- Materias con nombre y apellido de docentes -----
            var materias = new List<Materia>
            {
                new Materia { NombreMateria = "Matemática",  Docente = "Carlos López" },
                new Materia { NombreMateria = "Lenguaje",    Docente = "Ana Molina" },
                new Materia { NombreMateria = "Ciencias",    Docente = "José Díaz" },
                new Materia { NombreMateria = "Historia",    Docente = "Laura Aguilar" },
                new Materia { NombreMateria = "Inglés",      Docente = "David Cruz" }
            };
            ctx.Materias.AddRange(materias);
            await ctx.SaveChangesAsync();

            // Traer IDs generados
            var a = await ctx.Alumnos.OrderBy(x => x.AlumnoId).ToListAsync();
            var m = await ctx.Materias.OrderBy(x => x.MateriaId).ToListAsync();

            // ----- Expedientes de ejemplo -----
            var expedientes = new List<Expediente>
{
    new Expediente { AlumnoId = a[0].AlumnoId, MateriaId = m[0].MateriaId, NotaFinal = 9.5m, Observaciones = "Excelente" },
    new Expediente { AlumnoId = a[1].AlumnoId, MateriaId = m[1].MateriaId, NotaFinal = 8.8m },
    new Expediente { AlumnoId = a[2].AlumnoId, MateriaId = m[2].MateriaId, NotaFinal = 7.5m },
    new Expediente { AlumnoId = a[3].AlumnoId, MateriaId = m[3].MateriaId, NotaFinal = 9.0m, Observaciones = "Muy participativo" },
    new Expediente { AlumnoId = a[4].AlumnoId, MateriaId = m[4].MateriaId, NotaFinal = 8.2m },
    new Expediente { AlumnoId = a[5].AlumnoId, MateriaId = m[0].MateriaId, NotaFinal = 8.7m },
    new Expediente { AlumnoId = a[6].AlumnoId, MateriaId = m[2].MateriaId, NotaFinal = 9.1m, Observaciones = "Excelente análisis" },
    new Expediente { AlumnoId = a[7].AlumnoId, MateriaId = m[1].MateriaId, NotaFinal = 7.7m },
    new Expediente { AlumnoId = a[8].AlumnoId, MateriaId = m[3].MateriaId, NotaFinal = 8.9m }
};

            foreach (var e in expedientes)
            {
                bool existe = await ctx.Expedientes
                    .AnyAsync(x => x.AlumnoId == e.AlumnoId && x.MateriaId == e.MateriaId);
                if (!existe)
                    ctx.Expedientes.Add(e);
            }

            await ctx.SaveChangesAsync();
        }
    }
}
