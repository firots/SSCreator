using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSBackground {
        [JsonConverter(typeof(StringEnumConverter))]
        public SSBackgroundType type;
        public string color;
        public string imagePath;
        public float? blur;
        public SSGradient? gradient;
    }

    public enum SSBackgroundType {
        Adaptive,
        Solid,
        Image,
        Gradient
    }
}
