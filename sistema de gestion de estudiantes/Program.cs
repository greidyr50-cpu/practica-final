using System;
using System.Windows.Forms;

namespace sistema_de_gestion_de_estudiantes
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cargar datos desde archivo; si no existe, inicializar con ejemplos
            GestorEstudiantes.CargarDesdeArchivo();
            if (!GestorEstudiantes.ObtenerTodos().GetEnumerator().MoveNext())
            {
                GestorEstudiantes.InicializarConEjemplos();
            }

            Application.Run(new Forms.MainForm());
        }
    }
}
