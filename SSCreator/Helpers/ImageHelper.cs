using System;
using System.IO;
using SkiaSharp;

namespace SSCreator {
    class ImageHelper {
        public SSModel model;

        public ImageHelper(SSModel model) {
            this.model = model;
        }
        
        public virtual void generate() {
            SKBitmap template = new SKBitmap(model.canvasSize[0], model.canvasSize[1]);
            using (SKCanvas canvas = new SKCanvas(template)) {
                string framePath = getSSCreatorPath(model.deviceFramePath);
                var frameBuffer = File.ReadAllBytes(framePath);
                SKBitmap frameBitmap = SKBitmap.Decode(frameBuffer);

                var ssBuffer = File.ReadAllBytes(model.screenshotPath);
                SKBitmap ssBitmap = SKBitmap.Decode(ssBuffer);

                var ssHeight = Convert.ToInt32(ssBitmap.Height * model.deviceFrameScale);
                var ssWidth = Convert.ToInt32(ssBitmap.Width * model.deviceFrameScale);
                var ssInfo = new SKImageInfo(ssWidth, ssHeight);
                var ssResized = ssBitmap.Resize(ssInfo, SKFilterQuality.High);

                var frameHeight = Convert.ToInt32(frameBitmap.Height * model.deviceFrameScale);
                var frameWidth = Convert.ToInt32(frameBitmap.Width * model.deviceFrameScale);
                var frameInfo = new SKImageInfo(frameWidth, frameHeight);
                var frameResized = frameBitmap.Resize(frameInfo, SKFilterQuality.High);

                var ssPosX = Convert.ToInt32(model.screenOffset[0] * model.deviceFrameScale) + model.devicePosition[0];
                var ssPosY = Convert.ToInt32(model.screenOffset[1] * model.deviceFrameScale) + model.devicePosition[1];

                canvas.DrawBitmap(ssResized, new SKPoint(ssPosX, ssPosY), null);
                canvas.DrawBitmap(frameResized, new SKPoint(model.devicePosition[0], model.devicePosition[1]), null);

                using (FileStream fs = new FileStream(model.savePath, FileMode.Create, FileAccess.Write)) {
                    SKData data = SKImage.FromBitmap(template).Encode(SKEncodedImageFormat.Png, 100);
                    var bytes = data.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ssCreator, path);
        }
    }
}