using System;
using System.Diagnostics;

namespace SSCreator {
    class Program {

        static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            CLParser.parse(args);
            stopwatch.Stop();
            Console.WriteLine("Whole process took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
        }
    }
}
