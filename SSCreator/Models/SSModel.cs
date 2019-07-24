using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SSCreator {
    public class SSModel {
        public SSSize canvasSize;
        public SSDevice device;
        public SSBackground background;
        public string screenshotPath;
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
                JObject o = JObject.Parse(json);
                JsonSerializer serializer = new JsonSerializer();
                SSModel model = (SSModel)serializer.Deserialize(new JTokenReader(o), typeof(SSModel));
                stopwatch.Stop();
                Console.WriteLine("JSON Loading took " + (Convert.ToDecimal(stopwatch.ElapsedMilliseconds) / 1000) + " seconds.");
                return model;
            } catch {
                return null;
            }
        }
    }
}
