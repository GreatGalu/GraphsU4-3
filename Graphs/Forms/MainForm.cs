using Graphs.Models;
using Graphs.Services;

namespace Graphs.Forms
{
    /// <summary>
    /// Formulario principal de la aplicación "Red Logística — Camino Más Corto".
    ///
    /// Principio de Responsabilidad Única (SRP):
    ///   Este formulario SOLO gestiona la interacción con el usuario (captura de datos,
    ///   actualización de controles y presentación de resultados). TODA la lógica de
    ///   grafos y el algoritmo de Dijkstra residen exclusivamente en GraphService.
    ///
    /// Flujo de datos:
    ///   Usuario → Formulario → GraphService.RunDijkstra() → DijkstraResult → Formulario
    /// </summary>
    public partial class MainForm : Form
    {
        // ──────────────────────────────────────────────────────────────────────────
        // Dependencia hacia la capa de servicios (inyección por constructor de campo)
        // ──────────────────────────────────────────────────────────────────────────
        private readonly GraphService _graphService;

        // ──────────────────────────────────────────────────────────────────────────
        // Constructor
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Inicializa el formulario y carga la red logística predefinida.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Crear el servicio con grafo DIRIGIDO (rutas de un solo sentido)
            _graphService = new GraphService(isDirected: true);

            // Cargar la UI con los datos iniciales del grafo
            RefreshVertexComboBoxes();
            RefreshAdjacencyMatrix();
            RefreshAdjacencyList();
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Manejadores de eventos de botones (SRP: solo delegan al servicio)
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Evento: el usuario presiona "Calcular Camino Más Corto".
        /// Delega el cálculo a GraphService y muestra el resultado en la UI.
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // ── Validar selección de vértices ──────────────────────────────────
            if (cboSource.SelectedIndex < 0 || cboDestination.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor selecciona el origen y el destino.",
                    "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sourceId      = cboSource.SelectedIndex;
            int destinationId = cboDestination.SelectedIndex;

            if (sourceId == destinationId)
            {
                MessageBox.Show("El origen y el destino deben ser diferentes.",
                    "Selección inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ── Ejecutar Dijkstra (lógica 100% en GraphService, no aquí) ───────
            DijkstraResult result = _graphService.RunDijkstra(sourceId, destinationId);

            // ── Mostrar la traza de ejecución en el ListBox ────────────────────
            lstTrace.Items.Clear();
            foreach (string line in result.ExecutionTrace)
                lstTrace.Items.Add(line);

            // ── Mostrar el resultado final ──────────────────────────────────────
            if (result.PathFound)
            {
                string pathStr = string.Join(" → ", result.ShortestPath.Select(v => v.Name));
                lblResult.Text      = $"✔ Camino: {pathStr}";
                lblCost.Text        = $"Costo total: {result.TotalCost} km";
                lblResult.ForeColor = Color.DarkGreen;
                lblCost.ForeColor   = Color.DarkGreen;
            }
            else
            {
                lblResult.Text      = "✗ No existe camino entre los vértices seleccionados.";
                lblCost.Text        = "";
                lblResult.ForeColor = Color.DarkRed;
            }
        }

        /// <summary>
        /// Evento: el usuario presiona "Agregar Vértice".
        /// Valida el nombre ingresado y delega la creación al servicio.
        /// </summary>
        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            string name = txtVertexName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Escribe el nombre del vértice (ciudad/nodo).",
                    "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar que no exista ya un vértice con el mismo nombre
            if (_graphService.Vertices.Any(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Ya existe un vértice llamado '{name}'.",
                    "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Delegar la adición al servicio
            _graphService.AddVertex(name);

            // Actualizar controles de UI
            txtVertexName.Clear();
            RefreshVertexComboBoxes();
            RefreshAdjacencyMatrix();
            RefreshAdjacencyList();

            MessageBox.Show($"Vértice '{name}' agregado correctamente.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Evento: el usuario presiona "Agregar Arista".
        /// Valida los campos y delega la creación de la arista al servicio.
        /// </summary>
        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            // ── Validar selecciones y peso ─────────────────────────────────────
            if (cboEdgeSource.SelectedIndex < 0 || cboEdgeDestination.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona el origen y destino de la arista.",
                    "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtEdgeWeight.Text.Trim(), out double weight) || weight < 0)
            {
                MessageBox.Show("El peso debe ser un número decimal no negativo.",
                    "Peso inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int edgeSourceId      = cboEdgeSource.SelectedIndex;
            int edgeDestinationId = cboEdgeDestination.SelectedIndex;

            if (edgeSourceId == edgeDestinationId)
            {
                MessageBox.Show("El origen y destino de la arista no pueden ser el mismo vértice.",
                    "Arista inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ── Delegar al servicio ────────────────────────────────────────────
            try
            {
                _graphService.AddEdge(edgeSourceId, edgeDestinationId, weight);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar arista: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ── Actualizar controles de UI ──────────────────────────────────────
            txtEdgeWeight.Clear();
            RefreshAdjacencyMatrix();
            RefreshAdjacencyList();

            string srcName  = _graphService.Vertices[edgeSourceId].Name;
            string dstName  = _graphService.Vertices[edgeDestinationId].Name;
            MessageBox.Show($"Arista {srcName} → {dstName} (peso: {weight}) agregada.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Evento: el usuario presiona "Reiniciar Grafo".
        /// Restablece la red logística predefinida.
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Deseas reiniciar el grafo con la red de ciudades predefinida?",
                "Confirmar reinicio",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _graphService.Reset();

            // Limpiar controles de resultados
            lstTrace.Items.Clear();
            lblResult.Text = "";
            lblCost.Text   = "";

            // Recargar la UI
            RefreshVertexComboBoxes();
            RefreshAdjacencyMatrix();
            RefreshAdjacencyList();
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Métodos de actualización de la UI (presentación pura, sin lógica de grafo)
        // ──────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Recarga los cuatro ComboBoxes de vértices con la lista actualizada.
        /// </summary>
        private void RefreshVertexComboBoxes()
        {
            var vertexNames = _graphService.Vertices.Select(v => v.Name).ToArray();

            // Guardar índices seleccionados para restaurarlos tras recargar
            int srcIdx      = cboSource.SelectedIndex;
            int dstIdx      = cboDestination.SelectedIndex;
            int eSrcIdx     = cboEdgeSource.SelectedIndex;
            int eDstIdx     = cboEdgeDestination.SelectedIndex;

            cboSource.Items.Clear();
            cboDestination.Items.Clear();
            cboEdgeSource.Items.Clear();
            cboEdgeDestination.Items.Clear();

            cboSource.Items.AddRange(vertexNames);
            cboDestination.Items.AddRange(vertexNames);
            cboEdgeSource.Items.AddRange(vertexNames);
            cboEdgeDestination.Items.AddRange(vertexNames);

            // Restaurar selecciones si siguen siendo válidas
            int maxIdx = vertexNames.Length - 1;
            cboSource.SelectedIndex      = srcIdx <= maxIdx  ? srcIdx  : -1;
            cboDestination.SelectedIndex = dstIdx <= maxIdx  ? dstIdx  : -1;
            cboEdgeSource.SelectedIndex  = eSrcIdx <= maxIdx ? eSrcIdx : -1;
            cboEdgeDestination.SelectedIndex = eDstIdx <= maxIdx ? eDstIdx : -1;
        }

        /// <summary>
        /// Actualiza el DataGridView con la Matriz de Adyacencia actual del grafo.
        /// Muestra "∞" en celdas sin arista y los pesos numéricos en el resto.
        /// </summary>
        private void RefreshAdjacencyMatrix()
        {
            double[,] matrix = _graphService.GetAdjacencyMatrix();
            int n            = _graphService.VertexCount;
            var vertices     = _graphService.Vertices;

            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();

            if (n == 0) return;

            // ── Crear columnas (encabezados = nombres de vértices) ─────────────
            // Primera columna: etiqueta de fila
            dgvMatrix.Columns.Add("rowLabel", "Ciudad");
            dgvMatrix.Columns[0].Width       = 140;
            dgvMatrix.Columns[0].ReadOnly    = true;
            dgvMatrix.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(230, 245, 230);
            dgvMatrix.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);

            for (int j = 0; j < n; j++)
            {
                int colIdx = dgvMatrix.Columns.Add($"col{j}", vertices[j].Name);
                dgvMatrix.Columns[colIdx].Width = 110;
                dgvMatrix.Columns[colIdx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // ── Crear filas ───────────────────────────────────────────────────
            for (int i = 0; i < n; i++)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dgvMatrix);
                row.Cells[0].Value = vertices[i].Name;   // Etiqueta de fila

                for (int j = 0; j < n; j++)
                {
                    double val = matrix[i, j];
                    if (i == j)
                    {
                        // Diagonal principal: sin auto-bucles
                        row.Cells[j + 1].Value = "—";
                        row.Cells[j + 1].Style.BackColor = Color.FromArgb(220, 220, 220);
                    }
                    else if (double.IsPositiveInfinity(val))
                    {
                        row.Cells[j + 1].Value = "∞";
                        row.Cells[j + 1].Style.ForeColor = Color.Gray;
                    }
                    else
                    {
                        row.Cells[j + 1].Value = val.ToString("0.##");
                        row.Cells[j + 1].Style.BackColor = Color.FromArgb(240, 255, 240);
                        row.Cells[j + 1].Style.ForeColor = Color.DarkGreen;
                        row.Cells[j + 1].Style.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    }
                }
                dgvMatrix.Rows.Add(row);
            }

            // Ajustar altura de filas
            foreach (DataGridViewRow r in dgvMatrix.Rows)
                r.Height = 26;
        }

        /// <summary>
        /// Actualiza el ListBox de la Lista de Adyacencia con la representación textual.
        /// </summary>
        private void RefreshAdjacencyList()
        {
            lstAdjacency.Items.Clear();
            foreach (string entry in _graphService.GetAdjacencyListRepresentation())
                lstAdjacency.Items.Add(entry);
        }
    }
}
