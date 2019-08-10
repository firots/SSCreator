using System;
using SkiaSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSText {
        public SSLine[] lines;
        public SSAlign alignX = new SSAlign();
        public SSAlign alignY = new SSAlign();
    }
}
