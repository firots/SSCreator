using System;
namespace SSCreator {
    public struct CLParser {
        public static bool parse(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("FError: args is empty.");
                return false;
            } else {
                SSModel model = SSModel.Load(args[0]);
                if (model != null) {
                    ImageHelper IH = new ImageHelper(model);
                    IH.generate();
                }
            }
            return true;
        }
    }
}
