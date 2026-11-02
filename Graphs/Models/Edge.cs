namespace Graphs.Models
{
    /// <summary>
    /// Representa una arista ponderada y dirigida entre dos vértices del grafo.
    /// Modela la ruta/conexión entre dos ciudades o nodos de distribución con su costo asociado.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Vértice de origen de la arista (ciudad de partida).
        /// </summary>
        public Vertex Source { get; set; }

        /// <summary>
        /// Vértice de destino de la arista (ciudad de llegada).
        /// </summary>
        public Vertex Destination { get; set; }

        /// <summary>
        /// Peso o costo de la arista (distancia en km, tiempo en horas, costo monetario, etc.).
        /// El peso siempre debe ser un valor no negativo para compatibilidad con Dijkstra.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Constructor que inicializa la arista con su origen, destino y peso.
        /// </summary>
        /// <param name="source">Vértice origen de la conexión.</param>
        /// <param name="destination">Vértice destino de la conexión.</param>
        /// <param name="weight">Costo o distancia de la arista (debe ser ≥ 0).</param>
        public Edge(Vertex source, Vertex destination, double weight)
        {
            Source      = source;
            Destination = destination;
            Weight      = weight;
        }

        /// <summary>
        /// Representación textual de la arista para depuración y trazado del algoritmo.
        /// </summary>
        public override string ToString() =>
            $"{Source.Name} → {Destination.Name}  (peso: {Weight})";
    }
}
