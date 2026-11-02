namespace Graphs.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Variable requerida por el diseñador de Windows Forms.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Libera todos los recursos administrados y no administrados utilizados por el formulario.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método requerido para la compatibilidad con el diseñador de Windows Forms.
        /// No modificar el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pnlMain = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            grpAddVertex = new GroupBox();
            lblVertexName = new Label();
            txtVertexName = new TextBox();
            btnAddVertex = new Button();
            grpAddEdge = new GroupBox();
            lblEdgeSource = new Label();
            cboEdgeSource = new ComboBox();
            lblEdgeDestination = new Label();
            cboEdgeDestination = new ComboBox();
            lblEdgeWeight = new Label();
            txtEdgeWeight = new TextBox();
            btnAddEdge = new Button();
            grpDijkstra = new GroupBox();
            lblSource = new Label();
            cboSource = new ComboBox();
            lblDest = new Label();
            cboDestination = new ComboBox();
            btnCalculate = new Button();
            btnReset = new Button();
            lblResult = new Label();
            lblCost = new Label();
            grpMatrix = new GroupBox();
            dgvMatrix = new DataGridView();
            grpAdjList = new GroupBox();
            lstAdjacency = new ListBox();
            grpTrace = new GroupBox();
            lstTrace = new ListBox();
            pnlMain.SuspendLayout();
            grpAddVertex.SuspendLayout();
            grpAddEdge.SuspendLayout();
            grpDijkstra.SuspendLayout();
            grpMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).BeginInit();
            grpAdjList.SuspendLayout();
            grpTrace.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.AutoScroll = true;
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Controls.Add(lblSubtitle);
            pnlMain.Controls.Add(grpAddVertex);
            pnlMain.Controls.Add(grpAddEdge);
            pnlMain.Controls.Add(grpDijkstra);
            pnlMain.Controls.Add(grpMatrix);
            pnlMain.Controls.Add(grpAdjList);
            pnlMain.Controls.Add(grpTrace);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(12);
            pnlMain.Size = new Size(1264, 821);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 80, 140);
            lblTitle.Location = new Point(14, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(900, 34);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🗺  Red Logística — Camino Más Corto";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.FromArgb(90, 110, 135);
            lblSubtitle.Location = new Point(16, 50);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(900, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Algoritmo de Dijkstra — Grafo Ponderado Dirigido   |   .NET 8 / Windows Forms / C#";
            // 
            // grpAddVertex
            // 
            grpAddVertex.Controls.Add(lblVertexName);
            grpAddVertex.Controls.Add(txtVertexName);
            grpAddVertex.Controls.Add(btnAddVertex);
            grpAddVertex.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAddVertex.Location = new Point(14, 78);
            grpAddVertex.Name = "grpAddVertex";
            grpAddVertex.Size = new Size(380, 80);
            grpAddVertex.TabIndex = 2;
            grpAddVertex.TabStop = false;
            grpAddVertex.Text = "Agregar Ciudad / Vértice";
            // 
            // lblVertexName
            // 
            lblVertexName.Font = new Font("Segoe UI", 9F);
            lblVertexName.Location = new Point(10, 30);
            lblVertexName.Name = "lblVertexName";
            lblVertexName.Size = new Size(65, 22);
            lblVertexName.TabIndex = 0;
            lblVertexName.Text = "Nombre:";
            // 
            // txtVertexName
            // 
            txtVertexName.Location = new Point(80, 28);
            txtVertexName.Name = "txtVertexName";
            txtVertexName.PlaceholderText = "Ej: Monclova";
            txtVertexName.Size = new Size(180, 23);
            txtVertexName.TabIndex = 1;
            // 
            // btnAddVertex
            // 
            btnAddVertex.BackColor = Color.FromArgb(70, 130, 180);
            btnAddVertex.FlatStyle = FlatStyle.Flat;
            btnAddVertex.ForeColor = Color.White;
            btnAddVertex.Location = new Point(270, 26);
            btnAddVertex.Name = "btnAddVertex";
            btnAddVertex.Size = new Size(100, 28);
            btnAddVertex.TabIndex = 2;
            btnAddVertex.Text = "➕ Agregar";
            btnAddVertex.UseVisualStyleBackColor = false;
            btnAddVertex.Click += btnAddVertex_Click;
            // 
            // grpAddEdge
            // 
            grpAddEdge.Controls.Add(lblEdgeSource);
            grpAddEdge.Controls.Add(cboEdgeSource);
            grpAddEdge.Controls.Add(lblEdgeDestination);
            grpAddEdge.Controls.Add(cboEdgeDestination);
            grpAddEdge.Controls.Add(lblEdgeWeight);
            grpAddEdge.Controls.Add(txtEdgeWeight);
            grpAddEdge.Controls.Add(btnAddEdge);
            grpAddEdge.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAddEdge.Location = new Point(410, 78);
            grpAddEdge.Name = "grpAddEdge";
            grpAddEdge.Size = new Size(560, 80);
            grpAddEdge.TabIndex = 3;
            grpAddEdge.TabStop = false;
            grpAddEdge.Text = "Agregar Arista / Ruta";
            // 
            // lblEdgeSource
            // 
            lblEdgeSource.Font = new Font("Segoe UI", 9F);
            lblEdgeSource.Location = new Point(10, 30);
            lblEdgeSource.Name = "lblEdgeSource";
            lblEdgeSource.Size = new Size(55, 22);
            lblEdgeSource.TabIndex = 0;
            lblEdgeSource.Text = "Origen:";
            // 
            // cboEdgeSource
            // 
            cboEdgeSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEdgeSource.Location = new Point(70, 27);
            cboEdgeSource.Name = "cboEdgeSource";
            cboEdgeSource.Size = new Size(145, 23);
            cboEdgeSource.TabIndex = 1;
            // 
            // lblEdgeDestination
            // 
            lblEdgeDestination.Font = new Font("Segoe UI", 9F);
            lblEdgeDestination.Location = new Point(225, 30);
            lblEdgeDestination.Name = "lblEdgeDestination";
            lblEdgeDestination.Size = new Size(58, 22);
            lblEdgeDestination.TabIndex = 2;
            lblEdgeDestination.Text = "Destino:";
            // 
            // cboEdgeDestination
            // 
            cboEdgeDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEdgeDestination.Location = new Point(285, 27);
            cboEdgeDestination.Name = "cboEdgeDestination";
            cboEdgeDestination.Size = new Size(145, 23);
            cboEdgeDestination.TabIndex = 3;
            // 
            // lblEdgeWeight
            // 
            lblEdgeWeight.Font = new Font("Segoe UI", 9F);
            lblEdgeWeight.Location = new Point(10, 55);
            lblEdgeWeight.Name = "lblEdgeWeight";
            lblEdgeWeight.Size = new Size(70, 22);
            lblEdgeWeight.TabIndex = 4;
            lblEdgeWeight.Text = "Peso (km):";
            // 
            // txtEdgeWeight
            // 
            txtEdgeWeight.Location = new Point(85, 53);
            txtEdgeWeight.Name = "txtEdgeWeight";
            txtEdgeWeight.PlaceholderText = "Ej: 350";
            txtEdgeWeight.Size = new Size(80, 23);
            txtEdgeWeight.TabIndex = 5;
            // 
            // btnAddEdge
            // 
            btnAddEdge.BackColor = Color.FromArgb(60, 140, 60);
            btnAddEdge.FlatStyle = FlatStyle.Flat;
            btnAddEdge.ForeColor = Color.White;
            btnAddEdge.Location = new Point(440, 26);
            btnAddEdge.Name = "btnAddEdge";
            btnAddEdge.Size = new Size(110, 50);
            btnAddEdge.TabIndex = 6;
            btnAddEdge.Text = "➕ Agregar Arista";
            btnAddEdge.UseVisualStyleBackColor = false;
            btnAddEdge.Click += btnAddEdge_Click;
            // 
            // grpDijkstra
            // 
            grpDijkstra.Controls.Add(lblSource);
            grpDijkstra.Controls.Add(cboSource);
            grpDijkstra.Controls.Add(lblDest);
            grpDijkstra.Controls.Add(cboDestination);
            grpDijkstra.Controls.Add(btnCalculate);
            grpDijkstra.Controls.Add(btnReset);
            grpDijkstra.Controls.Add(lblResult);
            grpDijkstra.Controls.Add(lblCost);
            grpDijkstra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpDijkstra.Location = new Point(14, 170);
            grpDijkstra.Name = "grpDijkstra";
            grpDijkstra.Size = new Size(956, 90);
            grpDijkstra.TabIndex = 4;
            grpDijkstra.TabStop = false;
            grpDijkstra.Text = "Algoritmo de Dijkstra — Calcular Camino Más Corto";
            // 
            // lblSource
            // 
            lblSource.Font = new Font("Segoe UI", 9F);
            lblSource.Location = new Point(10, 30);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(100, 22);
            lblSource.TabIndex = 0;
            lblSource.Text = "Ciudad Origen:";
            // 
            // cboSource
            // 
            cboSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSource.Location = new Point(115, 27);
            cboSource.Name = "cboSource";
            cboSource.Size = new Size(170, 23);
            cboSource.TabIndex = 1;
            // 
            // lblDest
            // 
            lblDest.Font = new Font("Segoe UI", 9F);
            lblDest.Location = new Point(300, 30);
            lblDest.Name = "lblDest";
            lblDest.Size = new Size(105, 22);
            lblDest.TabIndex = 2;
            lblDest.Text = "Ciudad Destino:";
            // 
            // cboDestination
            // 
            cboDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDestination.Location = new Point(410, 27);
            cboDestination.Name = "cboDestination";
            cboDestination.Size = new Size(170, 23);
            cboDestination.TabIndex = 3;
            // 
            // btnCalculate
            // 
            btnCalculate.BackColor = Color.FromArgb(30, 80, 140);
            btnCalculate.FlatStyle = FlatStyle.Flat;
            btnCalculate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCalculate.ForeColor = Color.White;
            btnCalculate.Location = new Point(600, 24);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(155, 32);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "🔍 Calcular Camino";
            btnCalculate.UseVisualStyleBackColor = false;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(180, 60, 60);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(770, 24);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 32);
            btnReset.TabIndex = 5;
            btnReset.Text = "🔄 Reiniciar";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // lblResult
            // 
            lblResult.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResult.ForeColor = Color.DarkGreen;
            lblResult.Location = new Point(10, 62);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(650, 22);
            lblResult.TabIndex = 6;
            // 
            // lblCost
            // 
            lblCost.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCost.ForeColor = Color.DarkGreen;
            lblCost.Location = new Point(670, 62);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(270, 22);
            lblCost.TabIndex = 7;
            // 
            // grpMatrix
            // 
            grpMatrix.Controls.Add(dgvMatrix);
            grpMatrix.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpMatrix.Location = new Point(14, 272);
            grpMatrix.Name = "grpMatrix";
            grpMatrix.Size = new Size(956, 240);
            grpMatrix.TabIndex = 5;
            grpMatrix.TabStop = false;
            grpMatrix.Text = "Matriz de Adyacencia  (∞ = sin conexión directa)";
            // 
            // dgvMatrix
            // 
            dgvMatrix.AllowUserToAddRows = false;
            dgvMatrix.AllowUserToDeleteRows = false;
            dgvMatrix.BackgroundColor = Color.White;
            dgvMatrix.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 80, 140);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMatrix.ColumnHeadersHeight = 28;
            dgvMatrix.EnableHeadersVisualStyles = false;
            dgvMatrix.Location = new Point(8, 22);
            dgvMatrix.Name = "dgvMatrix";
            dgvMatrix.ReadOnly = true;
            dgvMatrix.RowHeadersVisible = false;
            dgvMatrix.Size = new Size(936, 208);
            dgvMatrix.TabIndex = 0;
            // 
            // grpAdjList
            // 
            grpAdjList.Controls.Add(lstAdjacency);
            grpAdjList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpAdjList.Location = new Point(14, 524);
            grpAdjList.Name = "grpAdjList";
            grpAdjList.Size = new Size(430, 260);
            grpAdjList.TabIndex = 6;
            grpAdjList.TabStop = false;
            grpAdjList.Text = "Lista de Adyacencia";
            // 
            // lstAdjacency
            // 
            lstAdjacency.BackColor = Color.FromArgb(250, 255, 250);
            lstAdjacency.BorderStyle = BorderStyle.None;
            lstAdjacency.Font = new Font("Consolas", 8.5F);
            lstAdjacency.HorizontalScrollbar = true;
            lstAdjacency.ItemHeight = 13;
            lstAdjacency.Location = new Point(8, 22);
            lstAdjacency.Name = "lstAdjacency";
            lstAdjacency.Size = new Size(410, 221);
            lstAdjacency.TabIndex = 0;
            // 
            // grpTrace
            // 
            grpTrace.Controls.Add(lstTrace);
            grpTrace.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpTrace.Location = new Point(456, 524);
            grpTrace.Name = "grpTrace";
            grpTrace.Size = new Size(514, 260);
            grpTrace.TabIndex = 7;
            grpTrace.TabStop = false;
            grpTrace.Text = "Traza de Ejecución — Algoritmo de Dijkstra (paso a paso)";
            // 
            // lstTrace
            // 
            lstTrace.BackColor = Color.FromArgb(252, 252, 248);
            lstTrace.BorderStyle = BorderStyle.None;
            lstTrace.Font = new Font("Consolas", 8F);
            lstTrace.HorizontalScrollbar = true;
            lstTrace.ItemHeight = 13;
            lstTrace.Location = new Point(8, 22);
            lstTrace.Name = "lstTrace";
            lstTrace.Size = new Size(494, 221);
            lstTrace.TabIndex = 0;
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1264, 821);
            Controls.Add(pnlMain);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1100, 720);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tarea 4.3 — Red Logística: Camino Más Corto (Dijkstra)";
            pnlMain.ResumeLayout(false);
            grpAddVertex.ResumeLayout(false);
            grpAddVertex.PerformLayout();
            grpAddEdge.ResumeLayout(false);
            grpAddEdge.PerformLayout();
            grpDijkstra.ResumeLayout(false);
            grpMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).EndInit();
            grpAdjList.ResumeLayout(false);
            grpTrace.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════════════════
        // Declaraciones de controles del formulario
        // ══════════════════════════════════════════════════════════════════════════
        private System.Windows.Forms.Panel           pnlMain;
        private System.Windows.Forms.Label           lblTitle;
        private System.Windows.Forms.Label           lblSubtitle;

        private System.Windows.Forms.GroupBox        grpAddVertex;
        private System.Windows.Forms.Label           lblVertexName;
        private System.Windows.Forms.TextBox         txtVertexName;
        private System.Windows.Forms.Button          btnAddVertex;

        private System.Windows.Forms.GroupBox        grpAddEdge;
        private System.Windows.Forms.Label           lblEdgeSource;
        private System.Windows.Forms.ComboBox        cboEdgeSource;
        private System.Windows.Forms.Label           lblEdgeDestination;
        private System.Windows.Forms.ComboBox        cboEdgeDestination;
        private System.Windows.Forms.Label           lblEdgeWeight;
        private System.Windows.Forms.TextBox         txtEdgeWeight;
        private System.Windows.Forms.Button          btnAddEdge;

        private System.Windows.Forms.GroupBox        grpDijkstra;
        private System.Windows.Forms.Label           lblSource;
        private System.Windows.Forms.ComboBox        cboSource;
        private System.Windows.Forms.Label           lblDest;
        private System.Windows.Forms.ComboBox        cboDestination;
        private System.Windows.Forms.Button          btnCalculate;
        private System.Windows.Forms.Button          btnReset;
        private System.Windows.Forms.Label           lblResult;
        private System.Windows.Forms.Label           lblCost;

        private System.Windows.Forms.GroupBox        grpMatrix;
        private System.Windows.Forms.DataGridView    dgvMatrix;

        private System.Windows.Forms.GroupBox        grpAdjList;
        private System.Windows.Forms.ListBox         lstAdjacency;

        private System.Windows.Forms.GroupBox        grpTrace;
        private System.Windows.Forms.ListBox         lstTrace;
    }
}
