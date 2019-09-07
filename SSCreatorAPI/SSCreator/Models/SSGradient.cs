using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public struct SSGradient {
        public string[] colors;
        [JsonConverter(typeof(StringEnumConverter))]
        public SSGradientDirection startPoint;
        [JsonConverter(typeof(StringEnumConverter))]
        public SSGradientDirection endPoint;
    }

    public enum SSGradientDirection {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
}
