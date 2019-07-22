using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System.IO;
using SixLabors.ImageSharp.Formats.Png;

namespace SSCreator
{
    class TemplateBase
    {
        public virtual string name => "Base";
        public virtual int argCount => 3;

        public virtual void generate(string[] args)
        {
            string templatePath = getTemplatePath();
            using (Image<Rgba32> template = Image.Load(Path.Combine(templatePath, "base.jpg")))
            using (Image<Rgba32> screenShot = Image.Load(args[1]))
            using (Image<Rgba32> outputImage = new Image<Rgba32>(1242, 2688))
            {
                screenShot.Mutate(o => o.Resize(new Size(570, 994)));
                outputImage.Mutate(o => o
                    .DrawImage(template, new Point(0, 0), 1f)
                    .DrawImage(screenShot, new Point(335, 339), 1f)
                );
                outputImage.Save(args[2]);
            }
        }

        private string getTemplatePath()
        {
            string temps = "SSCreatorFiles/Templates";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, temps, this.name);
        }
    }
}
