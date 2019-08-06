using System;
namespace SSCreator {
    public class SSLayer: IComparable<SSLayer> {
        public int id;
        public SSDevice[] devices;
        public SSText[] texts;
        
        public void setDeviceOffsets() {
            foreach (SSDevice device in devices) {
                device.setOffset();
            }
        }

        public int CompareTo(SSLayer compareLayer) {
            return this.id.CompareTo(compareLayer.id);
        }
    }
}
