using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

var summary = BenchmarkRunner.Run<MatrixMultiplierBenchmark>();


[ShortRunJob]
public class MatrixMultiplierBenchmark
{
    [Params(10, 20, 30, 40, 50, 60, 70, 80, 90, 100)]
    public int Size { get; set; }

    public Matrix M => new Matrix(Size, Size, true);

    [Benchmark]
    public IMatrix Parallel()
    {
        var m = new MatricesMultiplierParallel();
        return m.Multiply(M, M);
    }

    [Benchmark]
    public IMatrix NotParallel()
    {
        var m = new MatricesMultiplier();
        return m.Multiply(M, M);
    }
}


