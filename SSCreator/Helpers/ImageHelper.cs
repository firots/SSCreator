using System;
using System.IO;
using SkiaSharp;

namespace SSCreator {
    class ImageHelper {
        public SSModel model;

        public ImageHelper(SSModel model) {
            this.model = model;
        }
        
        public void generate() {
            SKBitmap template = new SKBitmap(model.canvasSize[0], model.canvasSize[1]);
            using (SKCanvas canvas = new SKCanvas(template)) {
                string framePath = getSSCreatorPath(model.device.framePath);
                var frameBuffer = File.ReadAllBytes(framePath);
                SKBitmap frameBitmap = SKBitmap.Decode(frameBuffer);

                var ssBuffer = File.ReadAllBytes(model.screenshotPath);
                SKBitmap ssBitmap = SKBitmap.Decode(ssBuffer);

                var ssHeight = Convert.ToInt32(ssBitmap.Height * model.device.frameScale);
                var ssWidth = Convert.ToInt32(ssBitmap.Width * model.device.frameScale);
                var ssInfo = new SKImageInfo(ssWidth, ssHeight);
                ssBitmap = ssBitmap.Resize(ssInfo, SKFilterQuality.High);

                var frameHeight = Convert.ToInt32(frameBitmap.Height * model.device.frameScale);
                var frameWidth = Convert.ToInt32(frameBitmap.Width * model.device.frameScale);
                var frameInfo = new SKImageInfo(frameWidth, frameHeight);
                var frameResized = frameBitmap.Resize(frameInfo, SKFilterQuality.Medium);


                var ssPosX = Convert.ToInt32(model.device.screenOffset[0] * model.device.frameScale) + model.device.position[0];
                var ssPosY = Convert.ToInt32(model.device.screenOffset[1] * model.device.frameScale) + model.device.position[1];

                canvas.DrawBitmap(ssBitmap, new SKPoint(ssPosX, ssPosY), null);
                canvas.DrawBitmap(frameResized, new SKPoint(model.device.position[0], model.device.position[1]), null);

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