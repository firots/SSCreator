﻿using System;
using SkiaSharp;

namespace SSCreator {
    class SSGenerator {
        public SSModel model;
        public SSGenerator(SSModel model) {
            this.model = model;
            model.setAutoValues();
        }

        public void generate() {
            SKBitmap SS = new SKBitmap(model.canvasSize.width, model.canvasSize.height);
            using (SKCanvas canvas = new SKCanvas(SS)) {
                SSBackgroundGenerator bgGenerator = new SSBackgroundGenerator(model.background, model.canvasSize);
                bgGenerator.drawBackground(canvas);
                drawLayers(canvas);
                SkiaHelper.saveBitmap(SS, model.savePath);
                Console.WriteLine("SS saved to " + model.savePath);
            }
        }

        private void drawLayers(SKCanvas canvas) {
            foreach(SSLayer layer in model.layers) {
                drawDevices(canvas, layer);
            }
        }

        private void drawDevices(SKCanvas canvas, SSLayer layer) {
            SSDeviceGenerator deviceGenerator = new SSDeviceGenerator(layer.devices, model.canvasSize);
            deviceGenerator.drawDevices(canvas);
        }
    }
}
