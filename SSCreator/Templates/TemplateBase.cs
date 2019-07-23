using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System.IO;
using SixLabors.ImageSharp.Formats.Png;
using SkiaSharp;

namespace SSCreator {
    class TemplateBase {

        public virtual string name => "Base";
        public virtual int argCount => 3;

        /*public virtual void generate(string[] args) { 
            string templatePath = getTemplatePath();
            using (Image<Rgba32> template = Image.Load(Path.Combine(templatePath, "base.jpg")))
            using (Image<Rgba32> screenShot = Image.Load(args[1]))
            using (Image<Rgba32> outputImage = new Image<Rgba32>(1242, 2688)) {
                screenShot.Mutate(o => o.Resize(new Size(570, 994)));
                outputImage.Mutate(o => o
                    .DrawImage(template, new Point(0, 0), 1f)
                    .DrawImage(screenShot, new Point(335, 339), 1f)
                );
                outputImage.Save(args[2]);
            }
        }*/

        public virtual void generate(string[] args) {
            SKBitmap template = new SKBitmap(1242, 2688);
            using (SKCanvas canvas = new SKCanvas(template)) {
                string templatePath = getTemplatePath();
                var baseBuffer = File.ReadAllBytes(Path.Combine(templatePath, "base.jpg"));
                SKBitmap baseBitMap = SKBitmap.Decode(baseBuffer);
                canvas.DrawBitmap(baseBitMap, new SKPoint(0, 0), null);

                var ssBuffer = File.ReadAllBytes(args[1]);
                SKBitmap ssBitMap = SKBitmap.Decode(ssBuffer);
                var info = new SKImageInfo(570, 994);
                var ssResized = ssBitMap.Resize(info, SKFilterQuality.High);

                canvas.DrawBitmap(baseBitMap, new SKPoint(0, 0), null);
                canvas.DrawBitmap(ssResized, new SKPoint(335, 339), null);

                using (FileStream fs = new FileStream(args[2], FileMode.Create, FileAccess.Write)) {
                    SKData data = SKImage.FromBitmap(template).Encode(SKEncodedImageFormat.Png, 100);
                    var bytes = data.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private string getTemplatePath() {
            string temps = "SSCreatorFiles/Templates";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, temps, this.name);
        }
    }
}