using System;
using SkiaSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSText {
        public SSAlign alignX = new SSAlign();
        public SSAlign alignY = new SSAlign();
        public int extraLineSpacing;
        public SSLine[] lines;
    }
}
