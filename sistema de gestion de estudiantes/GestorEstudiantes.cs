using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using sistema_de_gestion_de_estudiantes.Exceptions;

namespace sistema_de_gestion_de_estudiantes
{
    public static class GestorEstudiantes
    {
        private static readonly List<Estudiante> estudiantes = new List<Estudiante>();
        private const string dataFileName = "estudiantes.json";
        private static string DataFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFileName);

        public static IEnumerable<Estudiante> ObtenerTodos()
        {
            return estudiantes;
        }

        public static void Agregar(Estudiante e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            if (string.IsNullOrWhiteSpace(e.Id)) throw new ArgumentException("El ID no puede estar vacío.");
            if (estudiantes.Any(x => x.Id.Equals(e.Id, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Ya existe un estudiante con la misma matrícula/ID.");

            estudiantes.Add(e);
            GuardarEnArchivo();
        }

        public static Estudiante BuscarPorId(string id)
        {
            var encontrado = estudiantes.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (encontrado == null) throw new EstudianteNoEncontradoException($"Estudiante con ID '{id}' no encontrado.");
            return encontrado;
        }

        public static IEnumerable<Estudiante> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return Enumerable.Empty<Estudiante>();
            return estudiantes.Where(x => x.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public static void Actualizar(Estudiante actualizado)
        {
            if (actualizado == null) throw new ArgumentNullException(nameof(actualizado));
            var existente = estudiantes.FirstOrDefault(x => x.Id.Equals(actualizado.Id, StringComparison.OrdinalIgnoreCase));
            if (existente == null) throw new EstudianteNoEncontradoException($"Estudiante con ID '{actualizado.Id}' no encontrado.");

            existente.Nombre = actualizado.Nombre;
            existente.Edad = actualizado.Edad;
            existente.Sexo = actualizado.Sexo;
            existente.Carrera = actualizado.Carrera;
            existente.Estado = actualizado.Estado;
            existente.FechaInscripcion = actualizado.FechaInscripcion;
            GuardarEnArchivo();
        }

        public static void EliminarPorId(string id)
        {
            var existente = estudiantes.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (existente == null) throw new EstudianteNoEncontradoException($"Estudiante con ID '{id}' no encontrado.");
            estudiantes.Remove(existente);
            GuardarEnArchivo();
        }

        public static void InicializarConEjemplos()
        {
            if (estudiantes.Any()) return;

            estudiantes.Add(new Estudiante { Id = "A001", Nombre = "María Pérez", Edad = 20, Sexo = Sexo.Femenino, Carrera = "Ingeniería de Software", Estado = EstadoAcademico.Activo, FechaInscripcion = DateTime.Now.AddMonths(-6) });
            estudiantes.Add(new Estudiante { Id = "A002", Nombre = "Juan Gómez", Edad = 22, Sexo = Sexo.Masculino, Carrera = "Contabilidad", Estado = EstadoAcademico.Activo, FechaInscripcion = DateTime.Now.AddYears(-1) });
            estudiantes.Add(new Estudiante { Id = "A003", Nombre = "Ana Ruiz", Edad = 23, Sexo = Sexo.Femenino, Carrera = "Psicología", Estado = EstadoAcademico.Graduado, FechaInscripcion = DateTime.Now.AddYears(-4) });
            GuardarEnArchivo();
        }

        public static void GuardarEnArchivo()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(estudiantes, options);
                File.WriteAllText(DataFilePath, json);
            }
            catch
            {
                // En un escenario real reportar/registrar el error; aquí se ignora para no romper la UI
            }
        }

        public static void CargarDesdeArchivo()
        {
            try
            {
                if (!File.Exists(DataFilePath)) return;
                var json = File.ReadAllText(DataFilePath);
                var list = JsonSerializer.Deserialize<List<Estudiante>>(json);
                if (list == null) return;
                estudiantes.Clear();
                estudiantes.AddRange(list);
            }
            catch
            {
                // Ignorar errores de carga; la aplicación puede inicializar ejemplos
            }
        }
    }
}
