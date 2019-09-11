using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SkiaSharp;
namespace SSCreator {
    public class SSBackground {
        [JsonConverter(typeof(StringEnumConverter))]
        public SSBackgroundType type;
        public string color;
        public string imagePath;
        public float? blur;
        public SSGradient? gradient;
        public SKBitmap bitmap;
    }

    public enum SSBackgroundType {
        Adaptive,
        Solid,
        Image,
        Gradient
    }
}
