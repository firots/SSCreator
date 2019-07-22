using System;

namespace SSCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3) {
                Console.WriteLine("Error: Needs 3 arguments.");
                return;
            } 
            TemplateBase.generate(args[0], args[1], args[2]);
            Console.WriteLine("Image saved at" + args[2]);
        }
    }
}
