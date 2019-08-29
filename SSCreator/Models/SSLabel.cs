using System;
using SkiaSharp;
namespace SSCreator {
    public class SSLabel {
        public string text;
        public string fontName;
        public float fontSize;
        public string color;
        public byte alpha = 255;
        public SKFontStyleWeight styleWeight = SKFontStyleWeight.Normal;
        public SKFontStyleWidth styleWidth = SKFontStyleWidth.Normal;
        public SKFontStyleSlant styleSlant;
    }
}
