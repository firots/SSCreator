using System;
using System.IO;
namespace SSCreator {
    public struct CLParser {
        public static void parse(string[] args) {
            string json;
            if (args.Length == 0) {
                json = File.ReadAllText("test.json");
            } else {
                json = args[0];
            }
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
