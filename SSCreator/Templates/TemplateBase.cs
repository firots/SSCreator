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
        public static void generate(String templateName, String screenShotFilePath, String savePath)
        {
            String templatePath = getTemplatePath(templateName);
            using (Image<Rgba32> template = Image.Load(Path.Combine(templatePath, "base.jpg")))
            using (Image<Rgba32> screenShot = Image.Load(screenShotFilePath))
            using (Image<Rgba32> outputImage = new Image<Rgba32>(1242, 2688))
            {
                screenShot.Mutate(o => o.Resize(new Size(570, 994)));
                outputImage.Mutate(o => o
                    .DrawImage(template, new Point(0, 0), 1f)
                    .DrawImage(screenShot, new Point(335, 339), 1f)
                );
                outputImage.Save(savePath);
            }
        }

        private static String getTemplatePath(String templateName)
        {
            String temps = "SSCreatorFiles/Templates";
            String home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, temps, templateName);
        }
    }
}
