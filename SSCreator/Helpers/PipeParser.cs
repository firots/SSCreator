using System;
using System.Diagnostics;
using System.IO;
namespace SSCreator {
    public struct PipeParser {
        public static void parse(string json, StreamString ss) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            SSModel model = SSModel.load(json);
            if (model != null) {
                try {
                    SSGenerator SSG = new SSGenerator(model);
                    SSG.generate();
                    ss.WriteString("ss saved");
                } catch(Exception ex) {
                    Print.Error(ex.Message);
                    ss.WriteString("cannot generate ss");
                }
            } else {
                ss.WriteString("cannot parse json");
                Print.Error("Cannot parse JSON.");
            }
            stopwatch.Stop();
            Console.WriteLine("Whole process took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
        }
    }
}
