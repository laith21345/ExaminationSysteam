using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace ExaminationSysteam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject sub = new Subject(10,"c#");
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            sub.CreateExam();
            Console.Clear();
            sub.Start();
            Console.Clear();
            sub.ShowExam();
            sw.Stop();
            Console.WriteLine(sw);
        }
    }
}
