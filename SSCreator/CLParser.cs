using System;
namespace SSCreator
{
    public class CLParser
    {
        public static bool parse(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("Error: Empty args.");
            } else
            {
                foreach (TemplateBase t in TemplateManager.templates)
                {
                    if (t.name == args[0]) {
                        if (t.argCount == args.Length)
                        {
                            t.generate(args);
                            return true;
                        } else
                        {
                            Console.WriteLine("Error: " + t.name + " template requires " + t.argCount + "arguments.");
                        }
                        
                    }
                }
            }
            return false;
        }
    }
}
