namespace Implementación_de_casos_recursivos.Models
{
    public sealed class RecursionMetrics
    {
        public MemoryModelType MemoryModel { get; init; }
        public int DiskCount { get; init; }
        public int MaxCallDepth { get; init; }
        public long TotalRecursiveCalls { get; init; }
        public long TotalMoves { get; init; }
        public double ElapsedMilliseconds { get; init; }
        public long ElapsedTicks { get; init; }
        public long AllocatedBytes { get; init; }
        public string MemorySummary { get; init; } = string.Empty;
    }
}
