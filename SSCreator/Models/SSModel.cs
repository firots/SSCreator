using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSCreator {
    public class SSModel {
        public int[] canvasSize;
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
                JObject o = JObject.Parse(json);
                JsonSerializer serializer = new JsonSerializer();
                SSModel model = (SSModel)serializer.Deserialize(new JTokenReader(o), typeof(SSModel));
                return model;
            } catch {
                return null;
            }
        }
    }
}
