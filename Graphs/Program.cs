using Graphs.Forms;

namespace Graphs
{
    /// <summary>
    /// Punto de entrada de la aplicación "Red Logística — Camino Más Corto".
    /// Tarea 4.3: Modelado y Recorrido de Grafos con Algoritmo de Dijkstra.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación Windows Forms.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar propiedades de la aplicación (DPI, fuentes, etc.)
            ApplicationConfiguration.Initialize();

            // Iniciar la aplicación con el formulario principal (MainForm)
            Application.Run(new MainForm());
        }
    }
}
