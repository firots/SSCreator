using System;
namespace SSCreator {
    public struct CLParser {
        public static void parse(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("FError: args is empty.");
            } else {
                SSModel model = SSModel.load(args[0]);
                if (model != null) {
                    SSGenerator SSG = new SSGenerator(model);
                    SSG.generate();
                } else {
                    Console.WriteLine("Cannot parse JSON.");
                }
            }
        }
    }
}
