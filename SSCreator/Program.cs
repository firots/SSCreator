using System;
using System.Diagnostics;

namespace SSCreator {
    class Program {

        static void Main(string[] args) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            CLParser.parse(args);
            /*SSModel model = new SSModel();
            model.save("test.json");
            ImageHelper IH = new ImageHelper(model);
            IH.generate();*/
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

        }
    }
}
