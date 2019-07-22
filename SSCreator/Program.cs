using System;

namespace SSCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("Error: args is empty");
                return;
            } 
            TemplateBase.generate("Test", args[0], "result.png");
        }
    }
}
