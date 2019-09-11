using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkiaSharp;
namespace SSCreator {
    public class SSDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceModel model;
        public string framePath;
        public double frameScale;
        public SSAlign alignX = new SSAlign();
        public SSAlign alignY = new SSAlign();
        public SSPosition? screenOffset;
        public double? rotation;
        public SSSize? screenSize;
        public string screenshotPath;
        public bool? rightPart;
        public bool? adaptiveBackground;

        [JsonIgnore]
        public SKBitmap frame;

        [JsonIgnore]
        public SKBitmap screen;

        public void setOffset() {
            foreach (Device dev in Devices.all) {
                if (dev.name == model) {
                    if (screenOffset == null) {
                        screenOffset = dev.screenOffset;
                    }
                    if (screenSize == null) {
                        screenSize = dev.screenSize;
                    }
                }
            }
        }

        public async Task loadFrame() {
            var task = Task.Run(() => {
                var size = (SSSize)this.screenSize;
                var path = Path.Combine(Config.shared.appPath, "builder/public/static/frames", framePath);
                SKBitmap frameBitmap = SkiaHelper.createPersistentBitmap(path, size.width + 100, size.height + 100);
                frame = SkiaHelper.scaleBitmap(frameBitmap, frameScale);
            });
            await task;
        }

        public async Task loadScreen(SKCanvas canvas, SSBackgroundGenerator backgroundGenerator) {
            var task = Task.Run(() => {
                var size = (SSSize)screenSize;
                SKBitmap ssBitmap = SkiaHelper.createPersistentBitmap(screenshotPath, size.width, size.height);
                backgroundGenerator.drawAdaptiveBackground(canvas, ssBitmap);
                if (size.width != ssBitmap.Width || size.height != ssBitmap.Height) {
                    Print.Warning("Screenshot size is wrong, resizing screenshot...");
                    var info = new SKImageInfo(size.width, size.height);
                    ssBitmap = ssBitmap.Resize(info, SKFilterQuality.High);
                }
                screen = SkiaHelper.scaleBitmap(ssBitmap, frameScale);
            });
            await task;
        }
    }
}
