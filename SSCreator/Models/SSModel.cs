using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSCreator {
    public class SSModel {
        public SSSize canvasSize;
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
    }
}
