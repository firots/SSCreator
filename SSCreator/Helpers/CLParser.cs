using System;
namespace SSCreator {
    public struct CLParser {
        public static void parse(string[] args) {
            if (args.Length == 0) {
                Print.Error("args is empty.");
            } else {
                SSModel model = SSModel.load(args[0]);
                if (model != null) {
                    SSGenerator SSG = new SSGenerator(model);
                    SSG.generate();
                } else {
                    Print.Error("Cannot parse JSON.");
                }
            }
        }
    }
}
