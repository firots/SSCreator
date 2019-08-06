using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceModel model;
        public string framePath;
        public double frameScale;
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
        

    }
}
