using System;
using System.IO;
using System.Diagnostics;

namespace SSCreator {
    class Program {
        static void Main(string[] args) {
            //PipeServer.Run();
            Test();
        }

        static void Test() {
            string json = File.ReadAllText("test.json");
            SSModel model = SSModel.load(json);
            if (model != null) {
                    SSGenerator SSG = new SSGenerator(model);
                    SSG.generate();
            } else {
                Print.Error("Cannot parse JSON.");
            }
        }
    }
}
