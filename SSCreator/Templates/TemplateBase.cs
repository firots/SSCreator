using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Formats.Png;

namespace SSCreator
{
    class TemplateBase
    {
        public static void generate(String templateFileName, String screenShotFileName, String savePath)
        {
            using (Image<Rgba32> template = Image.Load(templateFileName))
            using (Image<Rgba32> screenShot = Image.Load(screenShotFileName))
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
    }
}
