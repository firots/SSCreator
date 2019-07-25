using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public struct SSBackground {
        [JsonConverter(typeof(StringEnumConverter))]
        public SSBackgroundType type;
        public int? color;
        public string imagePath;
        public float? blur;
    }

    public enum SSBackgroundType {
        Adaptive,
        Solid,
        Image
    }
}
