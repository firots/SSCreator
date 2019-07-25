using System;
using System.IO;
using SkiaSharp;

namespace SSCreator {
    class SSGenerator {
        public SSModel model;

        public SSGenerator(SSModel model) {
            this.model = model;
        }
        
        public void generate() {
            SKBitmap template = new SKBitmap(model.canvasSize.width, model.canvasSize.height);
            using (SKCanvas canvas = new SKCanvas(template)) {
                drawScreens(canvas);
                drawFrames(canvas);
                SkiaHelper.saveBitmap(template, model.savePath);
                Console.WriteLine("SS saved to " + model.savePath);
            }
        }

        private void drawBackground(SKCanvas canvas) {

        }

        private void drawAdaptiveBackground(SKCanvas canvas, SKBitmap bitmap) {
            if (bitmap.Width != model.canvasSize.width || bitmap.Height != model.canvasSize.height) {
                Print.Warning("Adaptive background size is not compatible with screenshot size, resizing adaptive background...");
                var info = new SKImageInfo(model.canvasSize.width, model.canvasSize.height);
                bitmap = bitmap.Resize(info, SKFilterQuality.High);
            }
            var filter = SKImageFilter.CreateBlur(model.background.blur ?? 20, model.background.blur ?? 20);
            var paint = new SKPaint {
                ImageFilter = filter
            };
            canvas.DrawBitmap(bitmap, new SKPoint(0, 0), paint);
        }

        private void drawScreens(SKCanvas canvas) {
            int index = 0;
            foreach (SSDevice device in model.devices) {
                var ssBuffer = File.ReadAllBytes(device.screenshotPath);
                SKBitmap ssBitmap = SKBitmap.Decode(ssBuffer);
                if (index == 0 && model.background.type == SSBackgroundType.Adaptive) {
                    drawAdaptiveBackground(canvas, ssBitmap);
                }
                if (device.screenSize.width != ssBitmap.Width || device.screenSize.height != ssBitmap.Height) {
                    Print.Warning("Screenshot size is wrong, resizing screenshot...");
                    var info = new SKImageInfo(device.screenSize.width, device.screenSize.height);
                    ssBitmap = ssBitmap.Resize(info, SKFilterQuality.High);
                }
                ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, device.frameScale);
                var ssPosX = Convert.ToInt32(device.screenOffset.x * device.frameScale) + device.position.x;
                var ssPosY = Convert.ToInt32(device.screenOffset.y * device.frameScale) + device.position.y;
                canvas.DrawBitmap(ssBitmap, new SKPoint(ssPosX, ssPosY), null);
                index++;
            }
        }

        private void drawFrames(SKCanvas canvas) {
            foreach (SSDevice device in model.devices) {
                string framePath = getSSCreatorPath(device.framePath);
                var frameBuffer = File.ReadAllBytes(framePath);
                SKBitmap frameBitmap = SKBitmap.Decode(frameBuffer);
                frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, device.frameScale);
                canvas.DrawBitmap(frameBitmap, new SKPoint(device.position.x, device.position.y), null);
            }
        }

        private string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ssCreator, path);
        }
    }
}