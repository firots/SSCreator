using System;
using SkiaSharp;
namespace SSCreator {

    public class SSDeviceGenerator {
        private SSDevice[] devices;
        private SSSize canvasSize;
        public SSDeviceGenerator(SSDevice[] devices, SSSize canvasSize) {
            this.devices = devices;
            this.canvasSize = canvasSize;
        }

        public void drawDevices(SKCanvas canvas) {
            int deviceId = 0;
            foreach (SSDevice device in devices) {
                SKBitmap deviceBitmap = createDevice(device, canvas, deviceId);
                var position = device.getPosition(deviceBitmap.Width, deviceBitmap.Height, canvasSize.width, canvasSize.height);
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
            SKBitmap ssBitmap = SKBitmap.Decode(device.screenshotPath);
            var screenSize = (SSSize)device.screenSize;
            if (device.adaptiveBackground == true) {
                drawAdaptiveBackground(canvas, ssBitmap);
            }
            if (screenSize.width != ssBitmap.Width || screenSize.height != ssBitmap.Height) {
                Print.Warning("Screenshot size is wrong, resizing screenshot...");
                var info = new SKImageInfo(screenSize.width, screenSize.height);
                ssBitmap = ssBitmap.Resize(info, SKFilterQuality.High);
            }
            ssBitmap = SkiaHelper.scaleBitmap(ssBitmap, device.frameScale);
            return ssBitmap;
        }

        private SKBitmap createFrame(SSDevice device) {
            string framePath = FileHelper.getSSCreatorPath(device.framePath);
            SKBitmap frameBitmap = SKBitmap.Decode(framePath);
            frameBitmap = SkiaHelper.scaleBitmap(frameBitmap, device.frameScale);
            return frameBitmap;
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
