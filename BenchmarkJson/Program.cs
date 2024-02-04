using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkJson;

class Program
{
    static void Main()
    {
        //var t = new Benchmark();
        //t.SystemJson_Deserialize_SourceGenerated();
        BenchmarkRunner.Run<Benchmark>();
    }
}