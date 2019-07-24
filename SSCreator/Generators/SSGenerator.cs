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
                drawScreen(canvas);
                drawFrame(canvas);
                SkiaHelper.saveBitmap(template, model.savePath);
            }
        }

        private void drawBackground(SKCanvas canvas) {

        }

        private void drawAdaptiveBackground(SKCanvas canvas, SKBitmap bitmap) {
            if (model.device.screenSize.width != model.canvasSize.width || model.device.screenSize.height != model.canvasSize.height) {
                var info = new SKImageInfo(model.canvasSize.width, model.canvasSize.height);
                bitmap = bitmap.Resize(info, SKFilterQuality.High);
            }
            var filter = SKImageFilter.CreateBlur(model.background.blur ?? 20, model.background.blur ?? 20);
            var paint = new SKPaint {
                ImageFilter = filter
            };
            canvas.DrawBitmap(bitmap, new SKPoint(0, 0), paint);
        }

        private void drawScreen(SKCanvas canvas) {
            var ssBuffer = File.ReadAllBytes(model.screenshotPath);
            SKBitmap ssBitmap = SKBitmap.Decode(ssBuffer);
            if (model.background.type == SSBackgroundType.Adaptive) {
                drawAdaptiveBackground(canvas, ssBitmap);
            }
            ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, model.device.frameScale);
            var ssPosX = Convert.ToInt32(model.device.screenOffset.x * model.device.frameScale) + model.device.position.x;
            var ssPosY = Convert.ToInt32(model.device.screenOffset.y * model.device.frameScale) + model.device.position.y;
            canvas.DrawBitmap(ssBitmap, new SKPoint(ssPosX, ssPosY), null);
        }

        private void drawFrame(SKCanvas canvas) {
            string framePath = getSSCreatorPath(model.device.framePath);
            var frameBuffer = File.ReadAllBytes(framePath);
            SKBitmap frameBitmap = SKBitmap.Decode(frameBuffer);
            frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, model.device.frameScale);
            canvas.DrawBitmap(frameBitmap, new SKPoint(model.device.position.x, model.device.position.y), null);
        }

        private string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ssCreator, path);
        }
    }
}