using Graphs.Models;

namespace Graphs.Services
{
    /// <summary>
    /// Servicio central de grafos: gestiona la estructura de datos del grafo ponderado dirigido,
    /// construye la Matriz de Adyacencia y la Lista de Adyacencia, y ejecuta el algoritmo
    /// de Dijkstra para encontrar el camino más corto entre dos vértices.
    ///
    /// Principio de Responsabilidad Única (SRP): Esta clase NO conoce ningún control de UI.
    /// Toda la lógica matemática y de estructura de datos reside aquí, desacoplada del formulario.
    /// </summary>
    public class GraphService
    {
        // ──────────────────────────────────────────────────────────────────────────
        // Constante centinela: representa la ausencia de arista entre dos vértices.
        // ──────────────────────────────────────────────────────────────────────────
        private const double NO_EDGE = double.PositiveInfinity;

        // ──────────────────────────────────────────────────────────────────────────
        // Estructuras de datos internas del grafo
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Colección de todos los vértices registrados en el grafo (acceso por índice/ID).
        /// </summary>
        private readonly List<Vertex> _vertices = new();

        /// <summary>
        /// Colección de todas las aristas registradas en el grafo (para la lista de adyacencia).
        /// </summary>
        private readonly List<Edge> _edges = new();

        /// <summary>
        /// Matriz de Adyacencia cuadrada de dimensión N×N donde N = número de vértices.
        /// _adjacencyMatrix[i][j] almacena el peso de la arista de i → j, o ∞ si no existe.
        /// Se reconstruye dinámicamente al agregar vértices o aristas.
        /// </summary>
        private double[,] _adjacencyMatrix = new double[0, 0];

        /// <summary>
        /// Indica si el grafo es dirigido (true) o no dirigido (false).
        /// En un grafo no dirigido, cada arista se registra en ambas direcciones.
        /// </summary>
        public bool IsDirected { get; private set; }

        // ──────────────────────────────────────────────────────────────────────────
        // Propiedades públicas de solo lectura
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>Devuelve una copia de la lista de vértices (inmutable desde el exterior).</summary>
        public IReadOnlyList<Vertex> Vertices => _vertices.AsReadOnly();

        /// <summary>Devuelve una copia de la lista de aristas (inmutable desde el exterior).</summary>
        public IReadOnlyList<Edge> Edges => _edges.AsReadOnly();

        /// <summary>Número actual de vértices en el grafo.</summary>
        public int VertexCount => _vertices.Count;

        // ──────────────────────────────────────────────────────────────────────────
        // Constructor
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Inicializa el servicio de grafos con la red de ciudades/rutas logísticas predefinida.
        /// </summary>
        /// <param name="isDirected">
        ///   true  → grafo dirigido (rutas de un solo sentido).
        ///   false → grafo no dirigido (rutas bidireccionales).
        /// </param>
        public GraphService(bool isDirected = true)
        {
            IsDirected = isDirected;
            LoadDefaultNetwork();
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Inicialización de la red de ejemplo
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Carga la red logística predefinida con 7 ciudades y sus conexiones ponderadas.
        /// Esta red modela rutas reales de distribución con distancias en kilómetros.
        ///
        ///   Ciudad 0: Ciudad de México (CDMX)
        ///   Ciudad 1: Guadalajara
        ///   Ciudad 2: Monterrey
        ///   Ciudad 3: Puebla
        ///   Ciudad 4: Querétaro
        ///   Ciudad 5: León
        ///   Ciudad 6: Mérida
        ///
        /// Estructura de aristas (dirigido, pesos en km):
        ///   CDMX → Guadalajara  500
        ///   CDMX → Querétaro    210
        ///   CDMX → Puebla       130
        ///   Guadalajara → León  150
        ///   Guadalajara → Monterrey 530
        ///   Querétaro → León    180
        ///   Querétaro → Monterrey 600
        ///   Puebla → Mérida     990
        ///   León → Monterrey    450
        ///   León → Mérida       1100
        ///   Monterrey → Mérida  1200
        /// </summary>
        private void LoadDefaultNetwork()
        {
            // ── 1. Registrar vértices ──────────────────────────────────────────
            AddVertex("Ciudad de México");
            AddVertex("Guadalajara");
            AddVertex("Monterrey");
            AddVertex("Puebla");
            AddVertex("Querétaro");
            AddVertex("León");
            AddVertex("Mérida");

            // ── 2. Registrar aristas ponderadas ───────────────────────────────
            AddEdge(0, 1, 500);   // CDMX → Guadalajara
            AddEdge(0, 4, 210);   // CDMX → Querétaro
            AddEdge(0, 3, 130);   // CDMX → Puebla
            AddEdge(1, 5, 150);   // Guadalajara → León
            AddEdge(1, 2, 530);   // Guadalajara → Monterrey
            AddEdge(4, 5, 180);   // Querétaro → León
            AddEdge(4, 2, 600);   // Querétaro → Monterrey
            AddEdge(3, 6, 990);   // Puebla → Mérida
            AddEdge(5, 2, 450);   // León → Monterrey
            AddEdge(5, 6, 1100);  // León → Mérida
            AddEdge(2, 6, 1200);  // Monterrey → Mérida
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Métodos de construcción del grafo
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Agrega un nuevo vértice al grafo y reconstruye la matriz de adyacencia.
        /// </summary>
        /// <param name="name">Nombre del vértice (ciudad, nodo logístico, etc.).</param>
        /// <returns>El vértice recién creado.</returns>
        public Vertex AddVertex(string name)
        {
            int newId   = _vertices.Count;
            var vertex  = new Vertex(newId, name);
            _vertices.Add(vertex);
            RebuildAdjacencyMatrix();
            return vertex;
        }

        /// <summary>
        /// Agrega una arista ponderada entre dos vértices identificados por su índice.
        /// Si el grafo es no dirigido, registra también la arista inversa.
        /// </summary>
        /// <param name="sourceId">Índice del vértice origen.</param>
        /// <param name="destinationId">Índice del vértice destino.</param>
        /// <param name="weight">Peso/costo de la arista (debe ser ≥ 0).</param>
        /// <exception cref="ArgumentException">Si algún índice está fuera de rango.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Si el peso es negativo.</exception>
        public void AddEdge(int sourceId, int destinationId, double weight)
        {
            // ── Validaciones de entrada ────────────────────────────────────────
            if (sourceId < 0 || sourceId >= _vertices.Count)
                throw new ArgumentException($"Índice de origen inválido: {sourceId}");
            if (destinationId < 0 || destinationId >= _vertices.Count)
                throw new ArgumentException($"Índice de destino inválido: {destinationId}");
            if (weight < 0)
                throw new ArgumentOutOfRangeException(nameof(weight),
                    "El peso de la arista no puede ser negativo (Dijkstra requiere pesos ≥ 0).");

            // ── Registro de la arista ──────────────────────────────────────────
            var source      = _vertices[sourceId];
            var destination = _vertices[destinationId];

            _edges.Add(new Edge(source, destination, weight));
            _adjacencyMatrix[sourceId, destinationId] = weight;

            // En grafo no dirigido se agrega la arista inversa
            if (!IsDirected)
            {
                _edges.Add(new Edge(destination, source, weight));
                _adjacencyMatrix[destinationId, sourceId] = weight;
            }
        }

        /// <summary>
        /// Reinicia el grafo a su estado vacío y recarga la red predefinida.
        /// Útil para restaurar el grafo de ejemplo desde la UI.
        /// </summary>
        public void Reset()
        {
            _vertices.Clear();
            _edges.Clear();
            _adjacencyMatrix = new double[0, 0];
            LoadDefaultNetwork();
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Representaciones de la estructura de datos
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Devuelve la Matriz de Adyacencia actual como un arreglo bidimensional de solo lectura.
        /// Utilizada por MainForm para mostrar la matriz en el DataGridView.
        /// </summary>
        public double[,] GetAdjacencyMatrix() => (double[,])_adjacencyMatrix.Clone();

        /// <summary>
        /// Genera la representación en texto de la Lista de Adyacencia para mostrar en la UI.
        /// Formato: "NombreOrigen: NombreDestino1(peso1), NombreDestino2(peso2), ..."
        /// </summary>
        public List<string> GetAdjacencyListRepresentation()
        {
            var result = new List<string>();
            int n      = _vertices.Count;

            for (int i = 0; i < n; i++)
            {
                var neighbors = new List<string>();
                for (int j = 0; j < n; j++)
                {
                    if (_adjacencyMatrix[i, j] < NO_EDGE)
                        neighbors.Add($"{_vertices[j].Name}({_adjacencyMatrix[i, j]})");
                }
                string neighborStr = neighbors.Count > 0
                    ? string.Join(", ", neighbors)
                    : "(sin conexiones)";
                result.Add($"{_vertices[i].Name}: {neighborStr}");
            }
            return result;
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Algoritmo de Dijkstra
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Ejecuta el Algoritmo de Dijkstra para encontrar el camino más corto
        /// entre un vértice origen y un vértice destino en el grafo ponderado.
        ///
        /// Complejidad temporal: O(V²) con la implementación de arreglo lineal usada aquí,
        /// donde V = número de vértices. Con cola de prioridad (min-heap) la complejidad
        /// mejora a O((V + E) log V), donde E = número de aristas.
        ///
        /// Precondición: Todos los pesos de las aristas deben ser ≥ 0.
        /// </summary>
        /// <param name="sourceId">Índice del vértice de origen.</param>
        /// <param name="destinationId">Índice del vértice de destino.</param>
        /// <returns>
        ///   Un <see cref="DijkstraResult"/> con el camino óptimo, el costo total
        ///   y la traza de ejecución paso a paso.
        /// </returns>
        public DijkstraResult RunDijkstra(int sourceId, int destinationId)
        {
            int    n      = _vertices.Count;
            var    result = new DijkstraResult();
            var    trace  = result.ExecutionTrace;

            // ── Inicialización ─────────────────────────────────────────────────
            // dist[i]     → distancia más corta conocida desde origen hasta vértice i
            // visited[i]  → true si el vértice i ya fue procesado (extraído del conjunto S)
            // previous[i] → predecesor de i en el camino más corto (para reconstrucción)
            double[] dist     = new double[n];
            bool[]   visited  = new bool[n];
            int[]    previous = new int[n];

            for (int i = 0; i < n; i++)
            {
                dist[i]     = NO_EDGE;   // distancia inicial = ∞
                visited[i]  = false;
                previous[i] = -1;        // sin predecesor conocido
            }
            dist[sourceId] = 0.0;        // distancia al origen = 0

            trace.Add("═══════════════════════════════════════════════════════");
            trace.Add($"  ALGORITMO DE DIJKSTRA — Red Logística de Ciudades");
            trace.Add("═══════════════════════════════════════════════════════");
            trace.Add($"  Origen : {_vertices[sourceId].Name}");
            trace.Add($"  Destino: {_vertices[destinationId].Name}");
            trace.Add("───────────────────────────────────────────────────────");
            trace.Add("  INICIALIZACIÓN:");
            trace.Add($"  dist[{_vertices[sourceId].Name}] = 0  |  todos los demás = ∞");
            trace.Add("───────────────────────────────────────────────────────");

            // ── Iteración principal del algoritmo ─────────────────────────────
            // Se repite V veces; en cada iteración se selecciona el vértice no visitado
            // con la distancia mínima conocida (relajación greedy).
            for (int iteration = 0; iteration < n; iteration++)
            {
                // Paso 1: Seleccionar el vértice u con dist mínima entre los no visitados
                int u = SelectMinDistanceVertex(dist, visited, n);
                if (u == -1) break;            // Todos los vértices alcanzables fueron procesados

                visited[u] = true;

                trace.Add($"");
                trace.Add($"  Iteración {iteration + 1}: Procesando → {_vertices[u].Name}");
                trace.Add($"  dist actual = {FormatDistance(dist[u])}");

                // Optimización de poda: si ya alcanzamos el destino con la mínima distancia,
                // podemos detener el algoritmo (solo válido para destino único).
                if (u == destinationId)
                {
                    trace.Add($"  ✔ Destino alcanzado. Terminando exploración anticipada.");
                    break;
                }

                // Paso 2: Relajar aristas de todos los vecinos v de u
                for (int v = 0; v < n; v++)
                {
                    // Ignorar si no hay arista u→v, o si v ya fue visitado
                    if (_adjacencyMatrix[u, v] >= NO_EDGE || visited[v]) continue;

                    double newDist = dist[u] + _adjacencyMatrix[u, v];

                    if (newDist < dist[v])
                    {
                        // Se encontró un camino más corto hacia v a través de u
                        trace.Add($"    Relajar arista: {_vertices[u].Name} → {_vertices[v].Name}");
                        trace.Add($"    dist[{_vertices[v].Name}]: {FormatDistance(dist[v])} → {newDist}  ✓ Actualizado");

                        dist[v]     = newDist;
                        previous[v] = u;
                    }
                    else
                    {
                        trace.Add($"    Arista: {_vertices[u].Name} → {_vertices[v].Name}  " +
                                  $"(costo: {_adjacencyMatrix[u, v]})  → No mejora ({FormatDistance(dist[v])})");
                    }
                }
            }

            // ── Reconstrucción del camino más corto ───────────────────────────
            result.TotalCost = dist[destinationId];

            trace.Add("");
            trace.Add("───────────────────────────────────────────────────────");
            trace.Add("  RECONSTRUCCIÓN DEL CAMINO:");

            if (!result.PathFound)
            {
                trace.Add($"  ✗ No existe camino entre {_vertices[sourceId].Name} y {_vertices[destinationId].Name}.");
            }
            else
            {
                result.ShortestPath = ReconstructPath(previous, sourceId, destinationId);
                string pathStr = string.Join(" → ", result.ShortestPath.Select(v => v.Name));
                trace.Add($"  Camino óptimo: {pathStr}");
                trace.Add($"  Costo total  : {result.TotalCost} km");
            }

            trace.Add("═══════════════════════════════════════════════════════");
            return result;
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Métodos auxiliares privados
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Selecciona el vértice no visitado con la menor distancia conocida.
        /// Implementación O(V) por búsqueda lineal (suficiente para grafos pequeños/medianos).
        /// </summary>
        private static int SelectMinDistanceVertex(double[] dist, bool[] visited, int n)
        {
            double minDist = NO_EDGE;
            int    minIdx  = -1;

            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && dist[i] < minDist)
                {
                    minDist = dist[i];
                    minIdx  = i;
                }
            }
            return minIdx;
        }

        /// <summary>
        /// Reconstruye la lista de vértices del camino más corto recorriendo
        /// el arreglo de predecesores desde el destino hacia el origen.
        /// </summary>
        private List<Vertex> ReconstructPath(int[] previous, int sourceId, int destinationId)
        {
            var path  = new List<Vertex>();
            int current = destinationId;

            // Recorre hacia atrás desde el destino hasta el origen
            while (current != -1)
            {
                path.Add(_vertices[current]);
                current = previous[current];
            }

            path.Reverse();   // Invertir para obtener el orden origen → destino

            // Verificar que el camino realmente comienza en el origen
            if (path.Count == 0 || path[0].Id != sourceId)
                return new List<Vertex>();

            return path;
        }

        /// <summary>
        /// Reconstruye la Matriz de Adyacencia cada vez que se modifica el grafo.
        /// Preserva los pesos de aristas existentes y extiende la matriz al nuevo tamaño.
        /// </summary>
        private void RebuildAdjacencyMatrix()
        {
            int n           = _vertices.Count;
            var newMatrix   = new double[n, n];

            // Inicializar toda la matriz con ∞ (ausencia de arista)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    newMatrix[i, j] = NO_EDGE;

            // Copiar pesos existentes de la matriz anterior
            int oldSize = _adjacencyMatrix.GetLength(0);
            for (int i = 0; i < oldSize && i < n; i++)
                for (int j = 0; j < oldSize && j < n; j++)
                    newMatrix[i, j] = _adjacencyMatrix[i, j];

            _adjacencyMatrix = newMatrix;
        }

        /// <summary>
        /// Formatea un valor de distancia para la traza: muestra "∞" si es infinito.
        /// </summary>
        private static string FormatDistance(double d) =>
            double.IsPositiveInfinity(d) ? "∞" : d.ToString("0.##");
    }
}
