namespace Graphs.Models
{
    /// <summary>
    /// Encapsula el resultado completo devuelto por el algoritmo de Dijkstra.
    /// Contiene la ruta óptima encontrada, el costo total y la traza detallada de ejecución.
    /// </summary>
    public class DijkstraResult
    {
        /// <summary>
        /// Lista ordenada de vértices que conforman el camino más corto
        /// desde el origen hasta el destino.
        /// </summary>
        public List<Vertex> ShortestPath { get; set; } = new();

        /// <summary>
        /// Costo total acumulado del camino más corto (suma de pesos de aristas recorridas).
        /// Contiene double.PositiveInfinity si no existe camino entre origen y destino.
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// Registro paso a paso de las decisiones tomadas por el algoritmo de Dijkstra.
        /// Cada entrada describe una iteración: vértice visitado, distancia actualizada, etc.
        /// Se muestra directamente en el ListBox de traza de la interfaz de usuario.
        /// </summary>
        public List<string> ExecutionTrace { get; set; } = new();

        /// <summary>
        /// Indica si existe un camino válido entre el origen y el destino seleccionados.
        /// </summary>
        public bool PathFound => TotalCost < double.PositiveInfinity;
    }
}
