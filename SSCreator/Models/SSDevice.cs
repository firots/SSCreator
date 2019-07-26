using System;
namespace SSCreator {
    public class SSDevice {
        public string name;
        public string framePath;
        public double frameScale;
        public SSPosition position;
        public SSPosition? screenOffset;
        public double? rotation;
        public SSSize screenSize;
        public string screenshotPath;

        public void setOffset() {
            switch (name) {
                case "Apple iPhone Xs Max":
                    screenOffset = new SSPosition(140, 140);
                    break;
                case "Apple iPhone 8 Plus":
                    screenOffset = new SSPosition(200, 400);
                    break;
                default:
                    break;
            }
        }
    }
}
