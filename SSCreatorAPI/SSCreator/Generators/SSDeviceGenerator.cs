using System;
using SkiaSharp;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSCreator {

    public class SSDeviceGenerator {
        private SSDevice[] devices;
        private SSSize canvasSize;
        public SSDeviceGenerator(SSDevice[] devices, SSSize canvasSize) {
            this.devices = devices;
            this.canvasSize = canvasSize;
        }

        public void drawDevices(SKCanvas canvas) {
            var tasks = new List<Task>();
            foreach (SSDevice device in devices) {
                tasks.Add(device.loadFrame());
                tasks.Add(device.loadScreen());
            }
            Task.WaitAll(tasks.ToArray());
            int deviceId = 0;
            foreach (SSDevice device in devices) {
                SKBitmap deviceBitmap = createDevice(device, canvas, deviceId);
                var position = PositionHelper.getPosition(device.alignX, device.alignY,deviceBitmap.Width, deviceBitmap.Height, canvasSize);
                if (device.rightPart == true) {
                    position.x = -(canvasSize.width - position.x);
                }
                canvas.DrawBitmap(deviceBitmap, new SKPoint(position.x, position.y));
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

        private SKBitmap createScreen(SSDevice device, SKCanvas canvas, int deviceId) {
            return device.screen;
            /*var screenSize = (SSSize)device.screenSize;
            SKBitmap ssBitmap = SkiaHelper.createPersistentBitmap(device.screenshotPath, screenSize.width, screenSize.height);
            if (device.adaptiveBackground == true) {
                drawAdaptiveBackground(canvas, ssBitmap);
            }
            if (screenSize.width != ssBitmap.Width || screenSize.height != ssBitmap.Height) {
                Print.Warning("Screenshot size is wrong, resizing screenshot...");
                var info = new SKImageInfo(screenSize.width, screenSize.height);
                ssBitmap = ssBitmap.Resize(info, SKFilterQuality.High);
            }
            ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, device.frameScale);
            return ssBitmap;*/
        }

        private SKBitmap createFrame(SSDevice device) {
            return device.frame;
            /*var screenSize = (SSSize)device.screenSize;
            var framePath = Path.Combine(Config.shared.appPath, "builder/public/static/frames", device.framePath);
            SKBitmap frameBitmap = SkiaHelper.createPersistentBitmap(framePath, screenSize.width + 100, screenSize.height + 100);
            frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, device.frameScale);
            return frameBitmap;*/
        }

        private void drawAdaptiveBackground(SKCanvas canvas, SKBitmap bitmap) {
            if (bitmap.Width != canvasSize.width || bitmap.Height != canvasSize.height) {
                Print.Warning("Adaptive background size is not compatible with screenshot size, resizing adaptive background...");
                var info = new SKImageInfo(canvasSize.width, canvasSize.height);
                bitmap = bitmap.Resize(info, SKFilterQuality.High);
            }
            var filter = SKImageFilter.CreateBlur(20, 20);
            var paint = new SKPaint {
                ImageFilter = filter
            };
            canvas.DrawBitmap(bitmap, new SKPoint(0, 0), paint);
        }
    }
}
