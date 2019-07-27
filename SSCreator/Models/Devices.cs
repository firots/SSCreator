using System;
namespace SSCreator {
    public class Devices {
        public static Device[] all = {
            new Device(DeviceModel.iPhone8Plus, new SSSize(1242,2208), new SSPosition(200,400)),
            new Device(DeviceModel.iPhoneXsMax, new SSSize(1242,2688), new SSPosition(140,140))
        };

    }

    public class Device {
        public DeviceModel name;
        public SSSize screenSize;
        public SSPosition screenOffset;

        public Device(DeviceModel name, SSSize screenSize, SSPosition screenOffset) {
            this.name = name;
            this.screenSize = screenSize;
            this.screenOffset = screenOffset;
        }
    }
}
