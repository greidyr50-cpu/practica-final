using System;

namespace sistema_de_gestion_de_estudiantes.Exceptions
{
    public class EstudianteNoEncontradoException : Exception
    {
        public EstudianteNoEncontradoException(string mensaje) : base(mensaje) { }
    }
}
