using System;
namespace SSCreator {
    public struct Print {
        public static void Error(string error) {
            Console.WriteLine("FError: " + error);
        }
        public static void Warning(string warning) {
            Console.WriteLine("FWarning: " + warning);
        }
    }
}
