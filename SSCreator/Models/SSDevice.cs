using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSDevice {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceModel model;
        public string framePath;
        public double frameScale;
        public SSPosition position;
        public SSPosition? screenOffset;
        public double? rotation;
        public SSSize screenSize;
        public string screenshotPath;
        public bool? rightPart;

        public void setOffset() {
            switch (model) {
                case DeviceModel.iPhoneXsMax:
                    screenOffset = new SSPosition(140, 140);
                    break;
                case DeviceModel.iPhone8Plus:
                    screenOffset = new SSPosition(200, 400);
                    break;
                default:
                    break;
            }
        }
    }

    public enum DeviceModel {
        iPhoneXsMax,
        iPhone8Plus
    }
}
