using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using Implementación_de_casos_recursivos.DynamicMemory;
using Implementación_de_casos_recursivos.Models;
using Implementación_de_casos_recursivos.Services;
using Implementación_de_casos_recursivos.StaticMemory;

namespace Implementación_de_casos_recursivos
{
    public partial class Form1 : Form
    {
        private readonly DepthBenchmarkService _benchmarkService = new DepthBenchmarkService();
        private readonly List<RecursionMetrics> _benchmarkResults = new List<RecursionMetrics>();

        private MoveStep[] _simulationMoves = Array.Empty<MoveStep>();
        private int _simulationIndex;
        private int _currentDiskCount = 4;
        private readonly Stack<int>[] _visualPegs =
        {
            new Stack<int>(),
            new Stack<int>(),
            new Stack<int>()
        };
        private bool _isPaused;
        private CancellationTokenSource? _benchmarkCts;

        public Form1()
        {
            InitializeComponent();
            ConfigureMonochromeGrid();
            cmbMemoryModel.SelectedIndex = 0;
            ResetSimulationState((int)numDisks.Value);
        }

        private void ConfigureMonochromeGrid()
        {
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvResults.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220);
            dgvResults.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            dgvResults.DefaultCellStyle.BackColor = Color.White;
            dgvResults.DefaultCellStyle.ForeColor = Color.Black;
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 215, 215);
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvResults.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            dgvResults.Columns.Clear();
            dgvResults.Columns.Add("Memoria", "Memoria");
            dgvResults.Columns.Add("Discos", "Discos");
            dgvResults.Columns.Add("ProfundidadMax", "Profundidad Max");
            dgvResults.Columns.Add("LlamadasRecursivas", "Llamadas Recursivas");
            dgvResults.Columns.Add("Movimientos", "Movimientos");
            dgvResults.Columns.Add("TiempoMs", "Tiempo (ms)");
            dgvResults.Columns.Add("Ticks", "Ticks");
            dgvResults.Columns.Add("BytesAsignados", "Bytes Asignados");
        }

        private void ResetSimulationState(int diskCount)
        {
            simTimer.Stop();
            _isPaused = false;
            _currentDiskCount = diskCount;
            _simulationMoves = Array.Empty<MoveStep>();
            _simulationIndex = 0;

            for (int i = 0; i < 3; i++)
            {
                _visualPegs[i].Clear();
            }

            for (int disk = diskCount; disk >= 1; disk--)
            {
                _visualPegs[0].Push(disk);
            }

            lstMoveLog.Items.Clear();
            btnStartSim.Enabled = true;
            btnPauseSim.Enabled = false;
            btnPauseSim.Text = "Pausar";
            numDisks.Enabled = true;
            cmbMemoryModel.Enabled = true;
            lblSimStatus.Text = $"Listo para simular {diskCount} discos.";
            picCanvas.Invalidate();
        }

        private void btnStartSim_Click(object? sender, EventArgs e)
        {
            int diskCount = (int)numDisks.Value;
            ResetSimulationState(diskCount);

            RecursionMetrics metrics;
            if (cmbMemoryModel.SelectedIndex == 0)
            {
                StaticHanoiSolver staticSolver = new StaticHanoiSolver();
                metrics = staticSolver.Solve(diskCount, true);
                _simulationMoves = staticSolver.RecordedMoves.ToArray();
            }
            else
            {
                DynamicHanoiSolver dynamicSolver = new DynamicHanoiSolver();
                metrics = dynamicSolver.Solve(diskCount, true);
                _simulationMoves = dynamicSolver.RecordedMoves.ToArray();
            }

            numDisks.Enabled = false;
            cmbMemoryModel.Enabled = false;
            btnStartSim.Enabled = false;
            btnPauseSim.Enabled = true;

            lblSimStatus.Text = $"Ejecutando ({metrics.MemoryModel}) | Profundidad max: {metrics.MaxCallDepth} | Llamadas: {metrics.TotalRecursiveCalls} | Tiempo calculo: {metrics.ElapsedMilliseconds:F4} ms";
            simTimer.Interval = trackSpeed.Value;
            simTimer.Start();
        }

        private void btnPauseSim_Click(object? sender, EventArgs e)
        {
            if (_isPaused)
            {
                _isPaused = false;
                btnPauseSim.Text = "Pausar";
                simTimer.Start();
            }
            else
            {
                _isPaused = true;
                btnPauseSim.Text = "Reanudar";
                simTimer.Stop();
            }
        }

        private void btnResetSim_Click(object? sender, EventArgs e)
        {
            ResetSimulationState((int)numDisks.Value);
        }

        private void trackSpeed_Scroll(object? sender, EventArgs e)
        {
            simTimer.Interval = trackSpeed.Value;
        }

        private void simTimer_Tick(object? sender, EventArgs e)
        {
            if (_simulationIndex >= _simulationMoves.Length)
            {
                simTimer.Stop();
                btnPauseSim.Enabled = false;
                btnStartSim.Enabled = true;
                numDisks.Enabled = true;
                cmbMemoryModel.Enabled = true;
                lblSimStatus.Text = $"Simulacion completada: {_simulationMoves.Length} movimientos totales.";
                return;
            }

            MoveStep step = _simulationMoves[_simulationIndex];
            _simulationIndex++;

            int movedDisk = _visualPegs[step.FromPeg].Pop();
            _visualPegs[step.ToPeg].Push(movedDisk);

            char fromLabel = (char)('A' + step.FromPeg);
            char toLabel = (char)('A' + step.ToPeg);
            lstMoveLog.Items.Add($"#{_simulationIndex:D4} Disco {step.Disk}: {fromLabel} -> {toLabel} (Prof: {step.CallDepth})");
            lstMoveLog.TopIndex = lstMoveLog.Items.Count - 1;

            lblSimStatus.Text = $"Paso {_simulationIndex}/{_simulationMoves.Length} | Disco {step.Disk}: {fromLabel} -> {toLabel} | Profundidad actual: {step.CallDepth}";
            picCanvas.Invalidate();
        }

        private void picCanvas_Resize(object? sender, EventArgs e)
        {
            picCanvas.Invalidate();
        }

        private void picCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int width = picCanvas.ClientSize.Width;
            int height = picCanvas.ClientSize.Height;
            if (width < 100 || height < 100)
            {
                return;
            }

            int baseY = height - 48;
            int pegHeight = Math.Max(80, height - 110);
            int marginX = Math.Max(40, width / 12);
            int usableWidth = width - marginX * 2;
            int pegSpacing = usableWidth / 3;

            using Pen basePen = new Pen(Color.Black, 3F);
            using Pen pegPen = new Pen(Color.FromArgb(80, 80, 80), 4F);
            using Pen diskBorderPen = new Pen(Color.Black, 1.5F);
            using Font labelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            using Font diskFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using StringFormat centerFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawLine(basePen, marginX / 2, baseY, width - marginX / 2, baseY);

            int maxDiskWidth = Math.Max(40, pegSpacing - 24);
            int minDiskWidth = Math.Max(22, maxDiskWidth / Math.Max(2, _currentDiskCount));
            int diskHeight = Math.Clamp((pegHeight - 20) / Math.Max(1, _currentDiskCount), 12, 28);

            for (int pegIndex = 0; pegIndex < 3; pegIndex++)
            {
                int centerX = marginX + pegSpacing * pegIndex + pegSpacing / 2;
                g.DrawLine(pegPen, centerX, baseY, centerX, baseY - pegHeight);

                string pegName = $"Torre {(char)('A' + pegIndex)}";
                g.DrawString(pegName, labelFont, Brushes.Black, new RectangleF(centerX - 50, baseY + 10, 100, 24), centerFormat);

                int[] disks = _visualPegs[pegIndex].ToArray();
                Array.Reverse(disks);

                for (int level = 0; level < disks.Length; level++)
                {
                    int diskSize = disks[level];
                    double ratio = _currentDiskCount == 1 ? 1.0 : (double)(diskSize - 1) / (_currentDiskCount - 1);
                    int diskWidth = (int)(minDiskWidth + ratio * (maxDiskWidth - minDiskWidth));

                    int x = centerX - diskWidth / 2;
                    int y = baseY - (level + 1) * diskHeight - 2;

                    int shade = 230 - (int)(ratio * 155);
                    using Brush diskBrush = new SolidBrush(Color.FromArgb(shade, shade, shade));
                    Rectangle rect = new Rectangle(x, y, diskWidth, diskHeight - 2);

                    g.FillRectangle(diskBrush, rect);
                    g.DrawRectangle(diskBorderPen, rect);

                    Brush textBrush = shade < 135 ? Brushes.White : Brushes.Black;
                    g.DrawString(diskSize.ToString(CultureInfo.InvariantCulture), diskFont, textBrush, rect, centerFormat);
                }
            }
        }

        private async void btnRunBenchmark_Click(object? sender, EventArgs e)
        {
            int minDepth = (int)numMinDepth.Value;
            int maxDepth = (int)numMaxDepth.Value;
            if (minDepth > maxDepth)
            {
                MessageBox.Show("La profundidad minima no puede ser mayor que la maxima.", "Rango invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkRecordMoves.Checked && maxDepth > 16)
            {
                MessageBox.Show("El registro de movimientos en memoria dinamica requiere demasiada memoria para profundidades mayores a 16. Desmarque la casilla o reduzca el maximo a 16.", "Advertencia de memoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _benchmarkResults.Clear();
            dgvResults.Rows.Clear();
            picChart.Invalidate();

            btnRunBenchmark.Enabled = false;
            btnCancelBenchmark.Enabled = true;
            btnExportCsv.Enabled = false;
            lblBenchmarkStatus.Text = "Ejecutando analisis de profundidad...";

            _benchmarkCts = new CancellationTokenSource();

            Progress<RecursionMetrics> progress = new Progress<RecursionMetrics>(metric =>
            {
                _benchmarkResults.Add(metric);
                dgvResults.Rows.Add(
                    metric.MemoryModel == MemoryModelType.Static ? "Estatica" : "Dinamica",
                    metric.DiskCount,
                    metric.MaxCallDepth,
                    metric.TotalRecursiveCalls.ToString("N0", CultureInfo.InvariantCulture),
                    metric.TotalMoves.ToString("N0", CultureInfo.InvariantCulture),
                    metric.ElapsedMilliseconds.ToString("F4", CultureInfo.InvariantCulture),
                    metric.ElapsedTicks.ToString("N0", CultureInfo.InvariantCulture),
                    metric.AllocatedBytes.ToString("N0", CultureInfo.InvariantCulture)
                );
                lblBenchmarkStatus.Text = $"Procesando profundidad {metric.DiskCount} ({metric.MemoryModel})...";
                picChart.Invalidate();
            });

            try
            {
                await _benchmarkService.RunDepthAnalysisAsync(
                    minDepth,
                    maxDepth,
                    chkRecordMoves.Checked,
                    progress,
                    _benchmarkCts.Token);

                lblBenchmarkStatus.Text = $"Analisis completado: {_benchmarkResults.Count} ejecuciones medidas ({minDepth} a {maxDepth} discos).";
                btnExportCsv.Enabled = _benchmarkResults.Count > 0;
            }
            catch (OperationCanceledException)
            {
                lblBenchmarkStatus.Text = "Analisis cancelado por el usuario.";
                btnExportCsv.Enabled = _benchmarkResults.Count > 0;
            }
            catch (Exception ex)
            {
                lblBenchmarkStatus.Text = $"Error: {ex.Message}";
            }
            finally
            {
                btnRunBenchmark.Enabled = true;
                btnCancelBenchmark.Enabled = false;
                _benchmarkCts?.Dispose();
                _benchmarkCts = null;
            }
        }

        private void btnCancelBenchmark_Click(object? sender, EventArgs e)
        {
            _benchmarkCts?.Cancel();
        }

        private void btnExportCsv_Click(object? sender, EventArgs e)
        {
            if (_benchmarkResults.Count == 0)
            {
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "resultados_profundidad_hanoi.csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Memoria,Discos,ProfundidadMax,LlamadasRecursivas,Movimientos,TiempoMs,Ticks,BytesAsignados");

            foreach (RecursionMetrics item in _benchmarkResults)
            {
                sb.AppendLine(string.Join(",",
                    item.MemoryModel.ToString(),
                    item.DiskCount.ToString(CultureInfo.InvariantCulture),
                    item.MaxCallDepth.ToString(CultureInfo.InvariantCulture),
                    item.TotalRecursiveCalls.ToString(CultureInfo.InvariantCulture),
                    item.TotalMoves.ToString(CultureInfo.InvariantCulture),
                    item.ElapsedMilliseconds.ToString("F6", CultureInfo.InvariantCulture),
                    item.ElapsedTicks.ToString(CultureInfo.InvariantCulture),
                    item.AllocatedBytes.ToString(CultureInfo.InvariantCulture)));
            }

            File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
            lblBenchmarkStatus.Text = $"Resultados exportados a {Path.GetFileName(dialog.FileName)}.";
        }

        private void picChart_Resize(object? sender, EventArgs e)
        {
            picChart.Invalidate();
        }

        private void picChart_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            int width = picChart.ClientSize.Width;
            int height = picChart.ClientSize.Height;
            if (width < 200 || height < 140)
            {
                return;
            }

            using Font titleFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using Font axisFont = new Font("Segoe UI", 8F, FontStyle.Regular);

            if (_benchmarkResults.Count == 0)
            {
                g.DrawString("Ejecute el analisis para visualizar la grafica Tiempo (ms) vs Profundidad.", axisFont, Brushes.DimGray, 16, 16);
                return;
            }

            int leftMargin = 72;
            int rightMargin = 28;
            int topMargin = 34;
            int bottomMargin = 42;

            int plotWidth = width - leftMargin - rightMargin;
            int plotHeight = height - topMargin - bottomMargin;
            if (plotWidth <= 20 || plotHeight <= 20)
            {
                return;
            }

            List<RecursionMetrics> staticPoints = _benchmarkResults
                .Where(m => m.MemoryModel == MemoryModelType.Static)
                .OrderBy(m => m.DiskCount)
                .ToList();

            List<RecursionMetrics> dynamicPoints = _benchmarkResults
                .Where(m => m.MemoryModel == MemoryModelType.Dynamic)
                .OrderBy(m => m.DiskCount)
                .ToList();

            int minDepth = _benchmarkResults.Min(m => m.DiskCount);
            int maxDepth = _benchmarkResults.Max(m => m.DiskCount);
            double maxMs = Math.Max(0.001, _benchmarkResults.Max(m => m.ElapsedMilliseconds));

            using Pen gridPen = new Pen(Color.FromArgb(230, 230, 230), 1F);
            using Pen axisPen = new Pen(Color.Black, 1.5F);

            for (int i = 0; i <= 4; i++)
            {
                int y = topMargin + (int)(plotHeight * (i / 4.0));
                g.DrawLine(gridPen, leftMargin, y, leftMargin + plotWidth, y);
                double val = maxMs * (1.0 - i / 4.0);
                g.DrawString(val.ToString("F2", CultureInfo.InvariantCulture), axisFont, Brushes.Black, 8, y - 7);
            }

            g.DrawLine(axisPen, leftMargin, topMargin, leftMargin, topMargin + plotHeight);
            g.DrawLine(axisPen, leftMargin, topMargin + plotHeight, leftMargin + plotWidth, topMargin + plotHeight);

            g.DrawString("Tiempo (ms) vs Profundidad de Llamada", titleFont, Brushes.Black, leftMargin, 8);

            int depthSpan = Math.Max(1, maxDepth - minDepth);
            for (int d = minDepth; d <= maxDepth; d++)
            {
                int x = leftMargin + (int)((double)(d - minDepth) / depthSpan * plotWidth);
                g.DrawLine(gridPen, x, topMargin, x, topMargin + plotHeight);
                g.DrawString(d.ToString(CultureInfo.InvariantCulture), axisFont, Brushes.Black, x - 8, topMargin + plotHeight + 6);
            }

            DrawSeries(g, staticPoints, minDepth, depthSpan, maxMs, leftMargin, topMargin, plotWidth, plotHeight, false);
            DrawSeries(g, dynamicPoints, minDepth, depthSpan, maxMs, leftMargin, topMargin, plotWidth, plotHeight, true);

            int legendX = leftMargin + 14;
            int legendY = topMargin + 8;
            using Pen staticLegendPen = new Pen(Color.Black, 2F);
            using Pen dynamicLegendPen = new Pen(Color.FromArgb(90, 90, 90), 2F) { DashStyle = DashStyle.Dash };

            g.DrawLine(staticLegendPen, legendX, legendY + 7, legendX + 24, legendY + 7);
            g.FillRectangle(Brushes.Black, legendX + 9, legendY + 4, 6, 6);
            g.DrawString("Memoria Estatica", axisFont, Brushes.Black, legendX + 30, legendY);

            g.DrawLine(dynamicLegendPen, legendX + 150, legendY + 7, legendX + 174, legendY + 7);
            g.DrawEllipse(staticLegendPen, legendX + 159, legendY + 4, 6, 6);
            g.DrawString("Memoria Dinamica", axisFont, Brushes.Black, legendX + 180, legendY);
        }

        private static void DrawSeries(
            Graphics g,
            List<RecursionMetrics> points,
            int minDepth,
            int depthSpan,
            double maxMs,
            int leftMargin,
            int topMargin,
            int plotWidth,
            int plotHeight,
            bool isDashed)
        {
            if (points.Count == 0)
            {
                return;
            }

            PointF[] coords = new PointF[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                float x = leftMargin + (float)((double)(points[i].DiskCount - minDepth) / depthSpan * plotWidth);
                float y = topMargin + plotHeight - (float)(points[i].ElapsedMilliseconds / maxMs * plotHeight);
                coords[i] = new PointF(x, y);
            }

            using Pen linePen = isDashed
                ? new Pen(Color.FromArgb(90, 90, 90), 2F) { DashStyle = DashStyle.Dash }
                : new Pen(Color.Black, 2F);

            if (coords.Length > 1)
            {
                g.DrawLines(linePen, coords);
            }

            foreach (PointF pt in coords)
            {
                if (isDashed)
                {
                    g.FillEllipse(Brushes.White, pt.X - 3.5F, pt.Y - 3.5F, 7F, 7F);
                    g.DrawEllipse(Pens.Black, pt.X - 3.5F, pt.Y - 3.5F, 7F, 7F);
                }
                else
                {
                    g.FillRectangle(Brushes.Black, pt.X - 3F, pt.Y - 3F, 6F, 6F);
                }
            }
        }
    }
}
