using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSBackground {
        [JsonConverter(typeof(StringEnumConverter))]
        public SSBackgroundType type = SSBackgroundType.Adaptive;
        public int? color;
        public string imagePath;
    }

    public enum SSBackgroundType {
        Adaptive,
        Solid,
        Image
    }
}
