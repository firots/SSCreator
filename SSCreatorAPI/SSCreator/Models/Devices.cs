using System;
namespace SSCreator {
    public class Devices {
        public static Device[] all = {
            new Device(DeviceModel.iPhone8Plus, new SSSize(1242,2208), new SSPosition(200,400)),
            new Device(DeviceModel.iPhoneXsMax, new SSSize(1242,2688), new SSPosition(140,140)),
            new Device(DeviceModel.iPadPro2nd, new SSSize(2048, 2732), new SSPosition(200,350)),
            new Device(DeviceModel.iPadPro3rd, new SSSize(2048, 2732), new SSPosition(200,200)),
        };
    }

    public enum DeviceModel {
        iPhoneXsMax,
        iPhone8Plus,
        iPadPro3rd,
        iPadPro2nd
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
