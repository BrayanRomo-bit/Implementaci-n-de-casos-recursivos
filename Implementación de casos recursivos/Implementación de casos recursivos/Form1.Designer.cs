namespace Implementación_de_casos_recursivos
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabMain;
        private TabPage tabSimulation;
        private TabPage tabBenchmark;

        private Panel pnlSimControls;
        private Label lblDisks;
        private NumericUpDown numDisks;
        private Label lblMemoryModel;
        private ComboBox cmbMemoryModel;
        private Label lblSpeed;
        private TrackBar trackSpeed;
        private Button btnStartSim;
        private Button btnPauseSim;
        private Button btnResetSim;

        private SplitContainer splitSim;
        private PictureBox picCanvas;
        private ListBox lstMoveLog;
        private Label lblSimStatus;

        private Panel pnlBenchControls;
        private Label lblMinDepth;
        private NumericUpDown numMinDepth;
        private Label lblMaxDepth;
        private NumericUpDown numMaxDepth;
        private CheckBox chkRecordMoves;
        private Button btnRunBenchmark;
        private Button btnCancelBenchmark;
        private Button btnExportCsv;

        private SplitContainer splitBench;
        private DataGridView dgvResults;
        private PictureBox picChart;
        private Label lblBenchmarkStatus;

        private System.Windows.Forms.Timer simTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabMain = new TabControl();
            tabSimulation = new TabPage();
            splitSim = new SplitContainer();
            picCanvas = new PictureBox();
            lstMoveLog = new ListBox();
            pnlSimControls = new Panel();
            btnResetSim = new Button();
            btnPauseSim = new Button();
            btnStartSim = new Button();
            trackSpeed = new TrackBar();
            lblSpeed = new Label();
            cmbMemoryModel = new ComboBox();
            lblMemoryModel = new Label();
            numDisks = new NumericUpDown();
            lblDisks = new Label();
            lblSimStatus = new Label();

            tabBenchmark = new TabPage();
            splitBench = new SplitContainer();
            dgvResults = new DataGridView();
            picChart = new PictureBox();
            pnlBenchControls = new Panel();
            btnExportCsv = new Button();
            btnCancelBenchmark = new Button();
            btnRunBenchmark = new Button();
            chkRecordMoves = new CheckBox();
            numMaxDepth = new NumericUpDown();
            lblMaxDepth = new Label();
            numMinDepth = new NumericUpDown();
            lblMinDepth = new Label();
            lblBenchmarkStatus = new Label();

            simTimer = new System.Windows.Forms.Timer(components);

            tabMain.SuspendLayout();
            tabSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitSim).BeginInit();
            splitSim.Panel1.SuspendLayout();
            splitSim.Panel2.SuspendLayout();
            splitSim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            pnlSimControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDisks).BeginInit();

            tabBenchmark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitBench).BeginInit();
            splitBench.Panel1.SuspendLayout();
            splitBench.Panel2.SuspendLayout();
            splitBench.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChart).BeginInit();
            pnlBenchControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinDepth).BeginInit();
            SuspendLayout();

            tabMain.Controls.Add(tabSimulation);
            tabMain.Controls.Add(tabBenchmark);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1084, 681);
            tabMain.TabIndex = 0;

            tabSimulation.BackColor = SystemColors.Control;
            tabSimulation.Controls.Add(splitSim);
            tabSimulation.Controls.Add(pnlSimControls);
            tabSimulation.Controls.Add(lblSimStatus);
            tabSimulation.Location = new Point(4, 24);
            tabSimulation.Name = "tabSimulation";
            tabSimulation.Padding = new Padding(8);
            tabSimulation.Size = new Size(1076, 653);
            tabSimulation.TabIndex = 0;
            tabSimulation.Text = "Simulacion Recursiva";

            pnlSimControls.Dock = DockStyle.Top;
            pnlSimControls.Location = new Point(8, 8);
            pnlSimControls.Name = "pnlSimControls";
            pnlSimControls.Size = new Size(1060, 52);
            pnlSimControls.TabIndex = 0;

            lblDisks.AutoSize = true;
            lblDisks.Location = new Point(6, 17);
            lblDisks.Name = "lblDisks";
            lblDisks.Size = new Size(118, 15);
            lblDisks.TabIndex = 0;
            lblDisks.Text = "Discos (profundidad):";

            numDisks.Location = new Point(130, 14);
            numDisks.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numDisks.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDisks.Name = "numDisks";
            numDisks.Size = new Size(55, 23);
            numDisks.TabIndex = 1;
            numDisks.Value = new decimal(new int[] { 4, 0, 0, 0 });

            lblMemoryModel.AutoSize = true;
            lblMemoryModel.Location = new Point(200, 17);
            lblMemoryModel.Name = "lblMemoryModel";
            lblMemoryModel.Size = new Size(58, 15);
            lblMemoryModel.TabIndex = 2;
            lblMemoryModel.Text = "Memoria:";

            cmbMemoryModel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMemoryModel.FormattingEnabled = true;
            cmbMemoryModel.Items.AddRange(new object[] { "Estatica", "Dinamica" });
            cmbMemoryModel.Location = new Point(264, 14);
            cmbMemoryModel.Name = "cmbMemoryModel";
            cmbMemoryModel.Size = new Size(105, 23);
            cmbMemoryModel.TabIndex = 3;

            lblSpeed.AutoSize = true;
            lblSpeed.Location = new Point(384, 17);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(65, 15);
            lblSpeed.TabIndex = 4;
            lblSpeed.Text = "Intervalo:";

            trackSpeed.Location = new Point(452, 10);
            trackSpeed.Maximum = 500;
            trackSpeed.Minimum = 15;
            trackSpeed.Name = "trackSpeed";
            trackSpeed.Size = new Size(140, 45);
            trackSpeed.TabIndex = 5;
            trackSpeed.TickFrequency = 50;
            trackSpeed.Value = 150;
            trackSpeed.Scroll += trackSpeed_Scroll;

            btnStartSim.Location = new Point(606, 12);
            btnStartSim.Name = "btnStartSim";
            btnStartSim.Size = new Size(90, 28);
            btnStartSim.TabIndex = 6;
            btnStartSim.Text = "Iniciar";
            btnStartSim.UseVisualStyleBackColor = true;
            btnStartSim.Click += btnStartSim_Click;

            btnPauseSim.Enabled = false;
            btnPauseSim.Location = new Point(704, 12);
            btnPauseSim.Name = "btnPauseSim";
            btnPauseSim.Size = new Size(90, 28);
            btnPauseSim.TabIndex = 7;
            btnPauseSim.Text = "Pausar";
            btnPauseSim.UseVisualStyleBackColor = true;
            btnPauseSim.Click += btnPauseSim_Click;

            btnResetSim.Location = new Point(802, 12);
            btnResetSim.Name = "btnResetSim";
            btnResetSim.Size = new Size(90, 28);
            btnResetSim.TabIndex = 8;
            btnResetSim.Text = "Reiniciar";
            btnResetSim.UseVisualStyleBackColor = true;
            btnResetSim.Click += btnResetSim_Click;

            pnlSimControls.Controls.Add(btnResetSim);
            pnlSimControls.Controls.Add(btnPauseSim);
            pnlSimControls.Controls.Add(btnStartSim);
            pnlSimControls.Controls.Add(trackSpeed);
            pnlSimControls.Controls.Add(lblSpeed);
            pnlSimControls.Controls.Add(cmbMemoryModel);
            pnlSimControls.Controls.Add(lblMemoryModel);
            pnlSimControls.Controls.Add(numDisks);
            pnlSimControls.Controls.Add(lblDisks);

            splitSim.Dock = DockStyle.Fill;
            splitSim.Location = new Point(8, 60);
            splitSim.Name = "splitSim";
            splitSim.Panel1.Controls.Add(picCanvas);
            splitSim.Panel2.Controls.Add(lstMoveLog);
            splitSim.Size = new Size(1060, 560);
            splitSim.SplitterDistance = 740;
            splitSim.TabIndex = 1;

            picCanvas.BackColor = Color.White;
            picCanvas.BorderStyle = BorderStyle.FixedSingle;
            picCanvas.Dock = DockStyle.Fill;
            picCanvas.Location = new Point(0, 0);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(740, 560);
            picCanvas.TabIndex = 0;
            picCanvas.TabStop = false;
            picCanvas.Paint += picCanvas_Paint;
            picCanvas.Resize += picCanvas_Resize;

            lstMoveLog.BackColor = Color.White;
            lstMoveLog.BorderStyle = BorderStyle.FixedSingle;
            lstMoveLog.Dock = DockStyle.Fill;
            lstMoveLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lstMoveLog.ForeColor = Color.Black;
            lstMoveLog.FormattingEnabled = true;
            lstMoveLog.ItemHeight = 14;
            lstMoveLog.Location = new Point(0, 0);
            lstMoveLog.Name = "lstMoveLog";
            lstMoveLog.Size = new Size(316, 560);
            lstMoveLog.TabIndex = 0;

            lblSimStatus.BorderStyle = BorderStyle.FixedSingle;
            lblSimStatus.Dock = DockStyle.Bottom;
            lblSimStatus.Location = new Point(8, 620);
            lblSimStatus.Name = "lblSimStatus";
            lblSimStatus.Padding = new Padding(4, 3, 4, 3);
            lblSimStatus.Size = new Size(1060, 25);
            lblSimStatus.TabIndex = 2;
            lblSimStatus.Text = "Listo para simular.";
            lblSimStatus.TextAlign = ContentAlignment.MiddleLeft;

            tabBenchmark.BackColor = SystemColors.Control;
            tabBenchmark.Controls.Add(splitBench);
            tabBenchmark.Controls.Add(pnlBenchControls);
            tabBenchmark.Controls.Add(lblBenchmarkStatus);
            tabBenchmark.Location = new Point(4, 24);
            tabBenchmark.Name = "tabBenchmark";
            tabBenchmark.Padding = new Padding(8);
            tabBenchmark.Size = new Size(1076, 653);
            tabBenchmark.TabIndex = 1;
            tabBenchmark.Text = "Analisis por Profundidad";

            pnlBenchControls.Dock = DockStyle.Top;
            pnlBenchControls.Location = new Point(8, 8);
            pnlBenchControls.Name = "pnlBenchControls";
            pnlBenchControls.Size = new Size(1060, 52);
            pnlBenchControls.TabIndex = 0;

            lblMinDepth.AutoSize = true;
            lblMinDepth.Location = new Point(6, 17);
            lblMinDepth.Name = "lblMinDepth";
            lblMinDepth.Size = new Size(98, 15);
            lblMinDepth.TabIndex = 0;
            lblMinDepth.Text = "Profundidad min:";

            numMinDepth.Location = new Point(110, 14);
            numMinDepth.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numMinDepth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMinDepth.Name = "numMinDepth";
            numMinDepth.Size = new Size(58, 23);
            numMinDepth.TabIndex = 1;
            numMinDepth.Value = new decimal(new int[] { 3, 0, 0, 0 });

            lblMaxDepth.AutoSize = true;
            lblMaxDepth.Location = new Point(182, 17);
            lblMaxDepth.Name = "lblMaxDepth";
            lblMaxDepth.Size = new Size(100, 15);
            lblMaxDepth.TabIndex = 2;
            lblMaxDepth.Text = "Profundidad max:";

            numMaxDepth.Location = new Point(288, 14);
            numMaxDepth.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numMaxDepth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxDepth.Name = "numMaxDepth";
            numMaxDepth.Size = new Size(58, 23);
            numMaxDepth.TabIndex = 3;
            numMaxDepth.Value = new decimal(new int[] { 18, 0, 0, 0 });

            chkRecordMoves.AutoSize = true;
            chkRecordMoves.Location = new Point(362, 16);
            chkRecordMoves.Name = "chkRecordMoves";
            chkRecordMoves.Size = new Size(146, 19);
            chkRecordMoves.TabIndex = 4;
            chkRecordMoves.Text = "Registrar movimientos";
            chkRecordMoves.UseVisualStyleBackColor = true;

            btnRunBenchmark.Location = new Point(522, 12);
            btnRunBenchmark.Name = "btnRunBenchmark";
            btnRunBenchmark.Size = new Size(125, 28);
            btnRunBenchmark.TabIndex = 5;
            btnRunBenchmark.Text = "Ejecutar Analisis";
            btnRunBenchmark.UseVisualStyleBackColor = true;
            btnRunBenchmark.Click += btnRunBenchmark_Click;

            btnCancelBenchmark.Enabled = false;
            btnCancelBenchmark.Location = new Point(655, 12);
            btnCancelBenchmark.Name = "btnCancelBenchmark";
            btnCancelBenchmark.Size = new Size(90, 28);
            btnCancelBenchmark.TabIndex = 6;
            btnCancelBenchmark.Text = "Cancelar";
            btnCancelBenchmark.UseVisualStyleBackColor = true;
            btnCancelBenchmark.Click += btnCancelBenchmark_Click;

            btnExportCsv.Enabled = false;
            btnExportCsv.Location = new Point(753, 12);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(110, 28);
            btnExportCsv.TabIndex = 7;
            btnExportCsv.Text = "Exportar CSV";
            btnExportCsv.UseVisualStyleBackColor = true;
            btnExportCsv.Click += btnExportCsv_Click;

            pnlBenchControls.Controls.Add(btnExportCsv);
            pnlBenchControls.Controls.Add(btnCancelBenchmark);
            pnlBenchControls.Controls.Add(btnRunBenchmark);
            pnlBenchControls.Controls.Add(chkRecordMoves);
            pnlBenchControls.Controls.Add(numMaxDepth);
            pnlBenchControls.Controls.Add(lblMaxDepth);
            pnlBenchControls.Controls.Add(numMinDepth);
            pnlBenchControls.Controls.Add(lblMinDepth);

            splitBench.Dock = DockStyle.Fill;
            splitBench.Location = new Point(8, 60);
            splitBench.Name = "splitBench";
            splitBench.Orientation = Orientation.Horizontal;
            splitBench.Panel1.Controls.Add(dgvResults);
            splitBench.Panel2.Controls.Add(picChart);
            splitBench.Size = new Size(1060, 560);
            splitBench.SplitterDistance = 285;
            splitBench.TabIndex = 1;

            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.GridColor = Color.LightGray;
            dgvResults.Location = new Point(0, 0);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(1060, 285);
            dgvResults.TabIndex = 0;

            picChart.BackColor = Color.White;
            picChart.BorderStyle = BorderStyle.FixedSingle;
            picChart.Dock = DockStyle.Fill;
            picChart.Location = new Point(0, 0);
            picChart.Name = "picChart";
            picChart.Size = new Size(1060, 271);
            picChart.TabIndex = 0;
            picChart.TabStop = false;
            picChart.Paint += picChart_Paint;
            picChart.Resize += picChart_Resize;

            lblBenchmarkStatus.BorderStyle = BorderStyle.FixedSingle;
            lblBenchmarkStatus.Dock = DockStyle.Bottom;
            lblBenchmarkStatus.Location = new Point(8, 620);
            lblBenchmarkStatus.Name = "lblBenchmarkStatus";
            lblBenchmarkStatus.Padding = new Padding(4, 3, 4, 3);
            lblBenchmarkStatus.Size = new Size(1060, 25);
            lblBenchmarkStatus.TabIndex = 2;
            lblBenchmarkStatus.Text = "Seleccione un rango de profundidad y ejecute el analisis.";
            lblBenchmarkStatus.TextAlign = ContentAlignment.MiddleLeft;

            simTimer.Interval = 150;
            simTimer.Tick += simTimer_Tick;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1084, 681);
            Controls.Add(tabMain);
            ForeColor = Color.Black;
            MinimumSize = new Size(920, 620);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Torres de Hanoi - Recursion y Analisis de Profundidad";

            tabMain.ResumeLayout(false);
            tabSimulation.ResumeLayout(false);
            splitSim.Panel1.ResumeLayout(false);
            splitSim.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitSim).EndInit();
            splitSim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            pnlSimControls.ResumeLayout(false);
            pnlSimControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDisks).EndInit();

            tabBenchmark.ResumeLayout(false);
            splitBench.Panel1.ResumeLayout(false);
            splitBench.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitBench).EndInit();
            splitBench.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChart).EndInit();
            pnlBenchControls.ResumeLayout(false);
            pnlBenchControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinDepth).EndInit();
            ResumeLayout(false);
        }
    }
}
