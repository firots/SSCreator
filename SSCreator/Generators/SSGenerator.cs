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
            SKBitmap template = new SKBitmap(model.canvasSize[0], model.canvasSize[1]);
            using (SKCanvas canvas = new SKCanvas(template)) {
                drawScreen(canvas);
                drawFrame(canvas);
                SkiaHelper.saveBitmap(template, model.savePath);
            }
        }

        private void drawScreen(SKCanvas canvas) {
            var ssBuffer = File.ReadAllBytes(model.screenshotPath);
            SKBitmap ssBitmap = SKBitmap.Decode(ssBuffer);
            ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, model.device.frameScale);
            var ssPosX = Convert.ToInt32(model.device.screenOffset[0] * model.device.frameScale) + model.device.position[0];
            var ssPosY = Convert.ToInt32(model.device.screenOffset[1] * model.device.frameScale) + model.device.position[1];
            canvas.DrawBitmap(ssBitmap, new SKPoint(ssPosX, ssPosY), null);
        }

        private void drawFrame(SKCanvas canvas) {
            string framePath = getSSCreatorPath(model.device.framePath);
            var frameBuffer = File.ReadAllBytes(framePath);
            SKBitmap frameBitmap = SKBitmap.Decode(frameBuffer);
            frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, model.device.frameScale);
            canvas.DrawBitmap(frameBitmap, new SKPoint(model.device.position[0], model.device.position[1]), null);
        }

        private string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ssCreator, path);
        }
    }
}