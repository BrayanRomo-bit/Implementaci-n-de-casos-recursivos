using Implementación_de_casos_recursivos.DynamicMemory;
using Implementación_de_casos_recursivos.Models;
using Implementación_de_casos_recursivos.StaticMemory;

namespace Implementación_de_casos_recursivos.Services
{
    public sealed class DepthBenchmarkService
    {
        public async Task<IReadOnlyList<RecursionMetrics>> RunDepthAnalysisAsync(
            int minDepth,
            int maxDepth,
            bool recordMoves,
            IProgress<RecursionMetrics>? progress = null,
            CancellationToken cancellationToken = default)
        {
            if (minDepth < 1 || maxDepth < minDepth || maxDepth > StaticHanoiSolver.MaxSupportedDisks)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDepth), $"El rango de profundidad debe estar entre 1 y {StaticHanoiSolver.MaxSupportedDisks}.");
            }

            return await Task.Run(() =>
            {
                List<RecursionMetrics> results = new List<RecursionMetrics>();
                StaticHanoiSolver staticSolver = new StaticHanoiSolver();
                DynamicHanoiSolver dynamicSolver = new DynamicHanoiSolver();

                staticSolver.Solve(1, false, cancellationToken);
                dynamicSolver.Solve(1, false, cancellationToken);

                for (int depth = minDepth; depth <= maxDepth; depth++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    RecursionMetrics staticMetrics = staticSolver.Solve(depth, recordMoves, cancellationToken);
                    results.Add(staticMetrics);
                    progress?.Report(staticMetrics);

                    RecursionMetrics dynamicMetrics = dynamicSolver.Solve(depth, recordMoves, cancellationToken);
                    results.Add(dynamicMetrics);
                    progress?.Report(dynamicMetrics);
                }

                return (IReadOnlyList<RecursionMetrics>)results;
            }, cancellationToken);
        }
    }
}
