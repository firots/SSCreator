using System;

namespace SSCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            String img = "2.jpg";
            if (args.Length == 0) {
                img = "2.jpg";
            } else
            {
                img = args[0];
            }
            TemplateBase.generate("emptyIP7.jpg", img, "result.png");
        }
    }
}
