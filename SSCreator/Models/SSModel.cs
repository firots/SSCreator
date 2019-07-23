using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SSCreator {
    public class SSModel {
        /* Base */
        public int[] canvasSize = { 1242, 2688 };

        /* Frame */
        public string deviceFramePath = "Devices/Phones/Apple iPhone XS Max/Device/Apple iPhone XS Max Space Grey.png";
        public double deviceFrameScale = 0.75;
        public int[] devicePosition = { 50, 50 };
        public double[] screenOffset = { 140, 140 };

        /* Background */
        public int backgroundColor = 0xFF2400;
        public string backgroundImagePath;

        /* Screenshot */
        public string screenshotPath = "ss.png";

        /* Save Path */
        public string savePath = "result.png";

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
