using System;
using System.Diagnostics;
using System.IO;
namespace SSCreator {
    public struct CommandParser {
        public static CommandParserResult parse(string json) {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            CommandParserResult result  = new CommandParserResult();
            SSModel model = SSModel.load(json);
            if (model != null) {
                try {
                    SSGenerator SSG = new SSGenerator(model);
                    SSG.generate();
                    result.done = true;
                } catch(Exception ex) {
                    Print.Error(ex.Message);
                    result.done = false;
                    result.message = "cannot generate ss, check paths.";
                }
            } else {
                result.done = false;
                result.message = "cannot parse json.";
            }
            stopwatch.Stop();
            Console.WriteLine("whole process took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
            return result;
        }
    }

    public struct CommandParserResult
    {
        public bool done;
        public string message;
    }
}
