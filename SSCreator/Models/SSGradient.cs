using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public struct SSGradient {
        string[] colors;
        [JsonConverter(typeof(StringEnumConverter))]
        SSGradientDirection startPoint;
        [JsonConverter(typeof(StringEnumConverter))]
        SSGradientDirection endPoint;
    }

    public enum SSGradientDirection {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
}
