using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SSCreator {
    public class Config {
        public static Config shared = new Config();
        public string appPath;

        public static void load() {
            var jsonString = File.ReadAllText("config.json");
            shared = JsonConvert.DeserializeObject<Config>(jsonString);
        }
    }
}
