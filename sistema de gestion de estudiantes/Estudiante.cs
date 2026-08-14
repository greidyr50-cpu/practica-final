using System;

namespace sistema_de_gestion_de_estudiantes
{
    public enum Sexo { Masculino, Femenino }

    public enum EstadoAcademico { Activo, Inactivo, Graduado, Suspendido, Transferido }

    public class Estudiante
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public Sexo Sexo { get; set; }
        public string Carrera { get; set; }
        public EstadoAcademico Estado { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public Estudiante()
        {
            FechaInscripcion = DateTime.Now;
        }
    }
}
