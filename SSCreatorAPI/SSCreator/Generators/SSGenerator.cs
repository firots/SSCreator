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
            SKBitmap downScaled = SkiaHelper.scaleBitmap(SS, 0.3);
            SkiaHelper.saveBitmap(SS, model.savePath, model.encoding);
            Console.WriteLine("SS saved to " + model.savePath);
        }

        private void drawBackground(SKCanvas canvas) {
            SSBackgroundGenerator bgGenerator = new SSBackgroundGenerator(model.background, model.canvasSize);
            bgGenerator.drawBackground(canvas);
        }

        private void drawLayers(SKCanvas canvas) {
            foreach(SSLayer layer in model.layers) {
                drawShapes(canvas, layer);
                drawDevices(canvas, layer);
                drawTexts(canvas, layer);
            }
        }

        private void drawShapes(SKCanvas canvas, SSLayer layer) {
            if (layer.shapes != null && layer.shapes.Length > 0) {
                SSShapeGenerator shapeGenerator = new SSShapeGenerator(layer.shapes, model.canvasSize);
                shapeGenerator.drawShapes(canvas);
            }
        }

        private void drawDevices(SKCanvas canvas, SSLayer layer) {
            SSDeviceGenerator deviceGenerator = new SSDeviceGenerator(layer.devices, model.canvasSize);
            deviceGenerator.drawDevices(canvas);
        }

        private void drawTexts(SKCanvas canvas, SSLayer layer) {
            if (layer.texts != null && layer.texts.Length > 0) {
                SSTextGenerator textGenerator = new SSTextGenerator(layer.texts, model.canvasSize);
                textGenerator.drawTexts(canvas);
            }

        }
    }
}
