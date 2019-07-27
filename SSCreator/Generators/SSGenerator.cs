using System;
using SkiaSharp;

namespace SSCreator {
    class SSGenerator {
        public SSModel model;

        public SSGenerator(SSModel model) {
            this.model = model;
            model.setDeviceOffsets();
        }

        public void generate() {
            SKBitmap template = new SKBitmap(model.canvasSize.width, model.canvasSize.height);
            using (SKCanvas canvas = new SKCanvas(template)) {
                drawBackground(canvas);
                drawDevices(canvas);
                SkiaHelper.saveBitmap(template, model.savePath);
                Console.WriteLine("SS saved to " + model.savePath);
            }
        }

        private void drawBackground(SKCanvas canvas) {
            if (model.background.type == SSBackgroundType.solid) {
                drawSolidBackground(canvas);
            } else if (model.background.type == SSBackgroundType.image) {
                drawImageBackground(canvas);
            }
        }

        private void drawImageBackground(SKCanvas canvas) {
            SKBitmap bgBitmap = SKBitmap.Decode(model.background.imagePath);
            if (bgBitmap.Width != model.canvasSize.width || bgBitmap.Height != model.canvasSize.height) {
                var info = new SKImageInfo(model.canvasSize.width, model.canvasSize.height);
                bgBitmap = bgBitmap.Resize(info, SKFilterQuality.High);
            }
            if (model.background.blur.HasValue && model.background.blur != 0) {
                var filter = SKImageFilter.CreateBlur((int)model.background.blur, (int)model.background.blur);
                var paint = new SKPaint {
                    ImageFilter = filter
                };
                canvas.DrawBitmap(bgBitmap, new SKPoint(0, 0), paint);
            } else {
                canvas.DrawBitmap(bgBitmap, new SKPoint(0, 0));
            }
        }

        private void drawSolidBackground(SKCanvas canvas) {
            SKColor color;
            SKColor.TryParse(model.background.color.ToString(), out color);
            canvas.Clear(color);
        }

        private void drawDevices(SKCanvas canvas) {
            int deviceId = 0;
            foreach (SSDevice device in model.devices) {
                SKBitmap deviceBitmap = createDevice(device, canvas, deviceId);
                var x = device.position.x;
                if (device.rightPart == true) {
                    x = -(model.canvasSize.width - device.position.x);
                }
                canvas.DrawBitmap(deviceBitmap, new SKPoint(x, device.position.y));
                deviceId++;
            }
        }

        private SKBitmap createDevice(SSDevice device, SKCanvas canvas, int deviceId) {
            SKBitmap screenShot = createScreen(device, canvas, deviceId);
            SKBitmap frame = createFrame(device);
            var ssPosX = Convert.ToInt32(device.screenOffset?.x * device.frameScale);
            var ssPosY = Convert.ToInt32(device.screenOffset?.y * device.frameScale);
            Tuple<SKBitmap, SKPoint>[] bitMapsToCombine = {
                Tuple.Create(frame, new SKPoint(0, 0)),
                Tuple.Create(screenShot, new SKPoint(ssPosX, ssPosY))
            };
            SKBitmap deviceBitmap = SkiaHelper.overlayBitmaps(bitMapsToCombine);
            if (device.rotation.HasValue) {
                deviceBitmap = SkiaHelper.rotateBitmap(deviceBitmap, device.rotation ?? 0);
            }
            return deviceBitmap;
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

        private SKBitmap createScreen(SSDevice device, SKCanvas canvas, int deviceId) {
            SKBitmap ssBitmap = SKBitmap.Decode(device.screenshotPath);
            if (deviceId == 0 && model.background.type == SSBackgroundType.adaptive) {
                drawAdaptiveBackground(canvas, ssBitmap);
            }
            if (device.screenSize.width != ssBitmap.Width || device.screenSize.height != ssBitmap.Height) {
                Print.Warning("Screenshot size is wrong, resizing screenshot...");
                var info = new SKImageInfo(device.screenSize.width, device.screenSize.height);
                ssBitmap = ssBitmap.Resize(info, SKFilterQuality.High);
            }
            ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, device.frameScale);
            return ssBitmap;
        }
        
        private SKBitmap createFrame(SSDevice device) {
            string framePath = getSSCreatorPath(device.framePath);
            SKBitmap frameBitmap = SKBitmap.Decode(framePath);
            frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, device.frameScale);
            return frameBitmap;
        }

        private string getSSCreatorPath(string path) {
            string ssCreator = "SSCreator";
            string home = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return System.IO.Path.Combine(home, ssCreator, path);
        }
    }
}
