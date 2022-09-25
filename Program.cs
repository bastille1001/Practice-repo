using BenchmarkDotNet.Running;

namespace Nutshell.Study
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ExcelTask>();
        }
    }
}
