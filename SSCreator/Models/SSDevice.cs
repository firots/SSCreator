using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceModel model;
        public string framePath;
        public double frameScale;
        public SSPosition? position;
        public SSAlign alignX;
        public SSAlign alignY;
        public SSPosition? screenOffset;
        public double? rotation;
        public SSSize? screenSize;
        public string screenshotPath;
        public bool? rightPart;
        public bool? adaptiveBackground;

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
        
        public SSPosition getPosition(int deviceWidth, int deviceHeight, int canvasWidth, int canvasHeight) {
            var calculatedPos = new SSPosition(this.position?.x ?? 0, this.position?.y ?? 0);
            if (alignX != null) {
                if (alignX.style == AlignKeys.Center) {
                    calculatedPos.x = (canvasWidth - deviceWidth) / 2;
                } else if (alignX.style == AlignKeys.Left) {
                    calculatedPos.x = alignX.value;
                } else if (alignX.style == AlignKeys.Right) {
                    calculatedPos.x = canvasWidth - deviceWidth - alignX.value;
                }
            }
            if (alignY != null) {
                if (alignY.style == AlignKeys.Center) {
                    calculatedPos.y = (canvasHeight - deviceHeight) / 2;
                } else if (alignY.style == AlignKeys.Top) {
                    calculatedPos.y = alignY.value;
                } else if (alignY.style == AlignKeys.Bottom) {
                    calculatedPos.y = canvasHeight - deviceHeight - alignY.value;
                }
            }
            return calculatedPos;
        }
    }

    public enum AlignKeys {
        Left,
        Right,
        Top,
        Bottom,
        Center
    }

    public class SSAlign {
        [JsonConverter(typeof(StringEnumConverter))]
        public AlignKeys style = AlignKeys.Center;
        public int value = 0;
    }
}
