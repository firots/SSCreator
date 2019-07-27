using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSCreator {
    public class SSModel {
        public SSSize canvasSize = new SSSize(0, 0);
        public DeviceModel? canvasModel;
        public SSDevice[] devices;
        public SSBackground background;
        public string savePath;

        public void save(string path) {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                byte[] bytes = new UTF8Encoding(true).GetBytes(json);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static SSModel load(string json) {
            try {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var model = JsonConvert.DeserializeObject<SSModel>(json);
                stopwatch.Stop();
                Console.WriteLine("JSON Loading took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
                return model;
            } catch {
                return null;
            }
        }

        public void setAutoValues() {
            setDeviceOffsets();
            setCanvasSize();
        }

        private void setDeviceOffsets() {
            foreach (SSDevice device in devices) {
                if (device.screenOffset == null) {
                    device.setOffset();
                }
            }
        }

        private void setCanvasSize() {
            if (canvasSize.width == 0 && canvasSize.height == 0) {
                if (canvasModel.HasValue) {
                    switch (canvasModel) {
                        case DeviceModel.iPhoneXsMax:
                            canvasSize = new SSSize(1242, 2688);
                            break;
                        case DeviceModel.iPhone8Plus:
                            canvasSize = new SSSize(1242, 2208);
                            break;
                        default:
                            Print.Warning("Unknown canvasModel, canvas size will be zero.");
                            break;
                    }
                } else {
                    Print.Warning("Canvas size and canvasModels are empty, canvas size will be zero.");
                }
            }
        }
    }
}
