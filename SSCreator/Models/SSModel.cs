using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using SkiaSharp;

namespace SSCreator {
    public class SSModel {
        public SSSize canvasSize = new SSSize(0, 0);
        public DeviceModel? canvasModel;
        public SSBackground background;
        public string savePath;
        [JsonConverter(typeof(StringEnumConverter))]
        public SKEncodedImageFormat encoding;

        public List<SSLayer> layers = new List<SSLayer>();

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
                model.layers.Sort();
                return model;
            } catch {
                return null;
            }
        }

        public void setAutoValues() {
            setDeviceOffsets();
            setCanvasSize();
        }


        public void setDeviceOffsets() {
            foreach (SSLayer layer in layers) {
                layer.setDeviceOffsets();
            }
        }

        private void setCanvasSize() {
            if (canvasSize.width == 0 && canvasSize.height == 0) {
                if (canvasModel.HasValue) {
                    foreach (Device dev in Devices.all) {
                        if (dev.name == canvasModel) {
                            canvasSize = dev.screenSize;
                            return;
                        }
                    }
                } 
                Print.Warning("Canvas size and canvasModels are empty, canvas size will be zero.");
            }
        }
    }
}
