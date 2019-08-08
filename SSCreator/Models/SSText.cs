using System;
using SkiaSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSText {
        public string[] lines;
        public string fontName;
        public float fontSize;
        public string color;
        public SSAlign alignX = new SSAlign();
        public SSAlign alignY = new SSAlign();
        [JsonConverter(typeof(StringEnumConverter))]
        public SKTextAlign textAlign;

        public SSPosition getPosition(int textWidth, int textHeight, int canvasWidth, int canvasHeight) {
            return new SSPosition(600, 300);
        }
    }
}
