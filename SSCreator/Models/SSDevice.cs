using System;
namespace SSCreator {
    public struct SSDevice {
        public string framePath;
        public double frameScale;
        public SSPosition position;
        public SSPosition screenOffset;
        public double? rotation;
        public SSSize screenSize;
        public string screenshotPath;
    }
}
