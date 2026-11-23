using System.Diagnostics;
using Implementación_de_casos_recursivos.Models;

namespace Implementación_de_casos_recursivos.DynamicMemory
{
    public sealed class DynamicHanoiSolver
    {
        private Stack<int>[] _pegs = Array.Empty<Stack<int>>();
        private List<MoveStep> _recordedMoves = new List<MoveStep>();
        private long _totalMoves;
        private long _totalCalls;
        private int _maxDepth;

        public IReadOnlyList<MoveStep> RecordedMoves => _recordedMoves;

        public RecursionMetrics Solve(int diskCount, bool recordMoves, CancellationToken cancellationToken = default)
        {
            if (diskCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(diskCount), "El numero de discos debe ser mayor o igual a 1.");
            }

            long bytesBefore = GC.GetAllocatedBytesForCurrentThread();
            Stopwatch stopwatch = Stopwatch.StartNew();

            InitializeDynamicState(diskCount);
            ExecuteRecursive(diskCount, 0, 2, 1, 1, recordMoves, cancellationToken);

            stopwatch.Stop();
            long bytesAfter = GC.GetAllocatedBytesForCurrentThread();
            long allocated = Math.Max(0, bytesAfter - bytesBefore);

            return new RecursionMetrics
            {
                MemoryModel = MemoryModelType.Dynamic,
                DiskCount = diskCount,
                MaxCallDepth = _maxDepth,
                TotalRecursiveCalls = _totalCalls,
                TotalMoves = _totalMoves,
                ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds,
                ElapsedTicks = stopwatch.ElapsedTicks,
                AllocatedBytes = allocated,
                MemorySummary = $"Stack<int>[3] dinamico y List<MoveStep> ({_recordedMoves.Count} registros)"
            };
        }

        private void InitializeDynamicState(int diskCount)
        {
            _pegs = new[]
            {
                new Stack<int>(),
                new Stack<int>(),
                new Stack<int>()
            };

            for (int disk = diskCount; disk >= 1; disk--)
            {
                _pegs[0].Push(disk);
            }

            _recordedMoves = new List<MoveStep>();
            _totalMoves = 0;
            _totalCalls = 0;
            _maxDepth = 0;
        }

        private void ExecuteRecursive(int n, int source, int target, int auxiliary, int depth, bool recordMoves, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _totalCalls++;
            if (depth > _maxDepth)
            {
                _maxDepth = depth;
            }

            if (n == 1)
            {
                MoveDiskDynamic(source, target, depth, recordMoves);
                return;
            }

            ExecuteRecursive(n - 1, source, auxiliary, target, depth + 1, recordMoves, cancellationToken);
            MoveDiskDynamic(source, target, depth, recordMoves);
            ExecuteRecursive(n - 1, auxiliary, target, source, depth + 1, recordMoves, cancellationToken);
        }

        private void MoveDiskDynamic(int fromPeg, int toPeg, int depth, bool recordMoves)
        {
            int disk = _pegs[fromPeg].Pop();
            _pegs[toPeg].Push(disk);
            _totalMoves++;

            if (recordMoves)
            {
                _recordedMoves.Add(new MoveStep(disk, fromPeg, toPeg, depth));
            }
        }
    }
}
