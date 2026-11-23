using System.Diagnostics;
using Implementación_de_casos_recursivos.Models;

namespace Implementación_de_casos_recursivos.StaticMemory
{
    public sealed class StaticHanoiSolver
    {
        public const int MaxSupportedDisks = 28;
        public const int MaxRecordedMoves = 65535;

        private readonly int[,] _pegs = new int[3, MaxSupportedDisks];
        private readonly int[] _topIndices = new int[3];
        private readonly MoveStep[] _moveBuffer = new MoveStep[MaxRecordedMoves];

        private int _recordedMoveCount;
        private long _totalMoves;
        private long _totalCalls;
        private int _maxDepth;

        public ReadOnlySpan<MoveStep> RecordedMoves => _moveBuffer.AsSpan(0, _recordedMoveCount);
        public int RecordedMoveCount => _recordedMoveCount;

        public RecursionMetrics Solve(int diskCount, bool recordMoves, CancellationToken cancellationToken = default)
        {
            if (diskCount < 1 || diskCount > MaxSupportedDisks)
            {
                throw new ArgumentOutOfRangeException(nameof(diskCount), $"El numero de discos debe estar entre 1 y {MaxSupportedDisks}.");
            }

            InitializeStaticState(diskCount);

            long bytesBefore = GC.GetAllocatedBytesForCurrentThread();
            Stopwatch stopwatch = Stopwatch.StartNew();

            ExecuteRecursive(diskCount, 0, 2, 1, 1, recordMoves, cancellationToken);

            stopwatch.Stop();
            long bytesAfter = GC.GetAllocatedBytesForCurrentThread();
            long allocated = Math.Max(0, bytesAfter - bytesBefore);

            return new RecursionMetrics
            {
                MemoryModel = MemoryModelType.Static,
                DiskCount = diskCount,
                MaxCallDepth = _maxDepth,
                TotalRecursiveCalls = _totalCalls,
                TotalMoves = _totalMoves,
                ElapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds,
                ElapsedTicks = stopwatch.ElapsedTicks,
                AllocatedBytes = allocated,
                MemorySummary = $"Arreglos fijos int[3,{MaxSupportedDisks}] y buffer estatico[{MaxRecordedMoves}]"
            };
        }

        private void InitializeStaticState(int diskCount)
        {
            Array.Clear(_pegs, 0, _pegs.Length);
            _topIndices[0] = -1;
            _topIndices[1] = -1;
            _topIndices[2] = -1;

            for (int disk = diskCount; disk >= 1; disk--)
            {
                _topIndices[0]++;
                _pegs[0, _topIndices[0]] = disk;
            }

            _recordedMoveCount = 0;
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
                MoveDiskStatic(source, target, depth, recordMoves);
                return;
            }

            ExecuteRecursive(n - 1, source, auxiliary, target, depth + 1, recordMoves, cancellationToken);
            MoveDiskStatic(source, target, depth, recordMoves);
            ExecuteRecursive(n - 1, auxiliary, target, source, depth + 1, recordMoves, cancellationToken);
        }

        private void MoveDiskStatic(int fromPeg, int toPeg, int depth, bool recordMoves)
        {
            int disk = _pegs[fromPeg, _topIndices[fromPeg]];
            _pegs[fromPeg, _topIndices[fromPeg]] = 0;
            _topIndices[fromPeg]--;

            _topIndices[toPeg]++;
            _pegs[toPeg, _topIndices[toPeg]] = disk;

            _totalMoves++;

            if (recordMoves && _recordedMoveCount < MaxRecordedMoves)
            {
                _moveBuffer[_recordedMoveCount] = new MoveStep(disk, fromPeg, toPeg, depth);
                _recordedMoveCount++;
            }
        }
    }
}
