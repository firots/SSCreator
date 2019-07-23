using System;
namespace SSCreator {
    public struct CLParser {

        public static bool parse(string[] args) {
            if (args.Length > 0) {
                var template = Array.Find(TemplateManager.templates, t => t.name == args[0] && t.argCount == args.Length);
                if (template != null) {
                    template.generate(args);
                    return true;
                } 
            }
            Console.WriteLine("Error: Cannot find a template to match given arguments.");
            return false;
        }
    }
}
