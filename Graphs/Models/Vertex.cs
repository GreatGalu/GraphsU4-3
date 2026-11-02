namespace Graphs.Models
{
    /// <summary>
    /// Representa un vértice (nodo) dentro del grafo ponderado dirigido.
    /// Cada vértice corresponde a una ciudad o punto de distribución en la red logística.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// Identificador único numérico del vértice (índice en la matriz de adyacencia).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre descriptivo del vértice (nombre de la ciudad o nodo logístico).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor que inicializa el vértice con su identificador y nombre.
        /// </summary>
        /// <param name="id">Índice único del vértice dentro del grafo.</param>
        /// <param name="name">Nombre legible del vértice (ciudad, almacén, etc.).</param>
        public Vertex(int id, string name)
        {
            Id   = id;
            Name = name;
        }

        /// <summary>
        /// Representación textual del vértice para mostrar en listas y controles de UI.
        /// </summary>
        public override string ToString() => $"[{Id}] {Name}";
    }
}
