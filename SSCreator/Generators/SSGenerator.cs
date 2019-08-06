using System;
using SkiaSharp;

namespace SSCreator {
    class SSGenerator {
        private SSModel model;
        public SSGenerator(SSModel model) {
            this.model = model;
            model.setAutoValues();
        }

        public void generate() {
            SKBitmap SS = new SKBitmap(model.canvasSize.width, model.canvasSize.height);
            using (SKCanvas canvas = new SKCanvas(SS)) {
                drawBackground(canvas);
                drawLayers(canvas);
            }
            SkiaHelper.saveBitmap(SS, model.savePath);
            Console.WriteLine("SS saved to " + model.savePath);
        }

        private void drawBackground(SKCanvas canvas) {
            SSBackgroundGenerator bgGenerator = new SSBackgroundGenerator(model.background, model.canvasSize);
            bgGenerator.drawBackground(canvas);
        }

        private void drawLayers(SKCanvas canvas) {
            foreach(SSLayer layer in model.layers) {
                drawDevices(canvas, layer);
                drawTexts(canvas, layer);
            }
        }

        private void drawDevices(SKCanvas canvas, SSLayer layer) {
            SSDeviceGenerator deviceGenerator = new SSDeviceGenerator(layer.devices, model.canvasSize);
            deviceGenerator.drawDevices(canvas);
        }

        private void drawTexts(SKCanvas canvas, SSLayer layer) {
            SSTextGenerator textGenerator = new SSTextGenerator(layer.texts, model.canvasSize);
            textGenerator.drawTexts(canvas);
        }
    }
}
