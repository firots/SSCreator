using System;
namespace SSCreator
{
    public struct CLParser
    {
        public static bool Parse(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("Error: Empty args.");
                return false;
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
                            return false;
                        }
                        
                    }
                }
            }
            Console.WriteLine("Error: " + "template with name " + args[0] " not found");
            return false;
        }
    }
}
