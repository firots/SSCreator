using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class SSShape {
        [JsonConverter(typeof(StringEnumConverter))]
        public ShapeType type;
        [JsonConverter(typeof(StringEnumConverter))]
        public FillStyle fillStyle = FillStyle.Solid;
        public string fillColor;
        public SSAlign alignX = new SSAlign();
        public SSAlign alignY = new SSAlign();
        public int? diameter;
        public SSSize size;
        public bool? fillX;
        public bool? fillY;
        public SSGradient? gradient;


        public void setSize(SSSize canvasSize) {
            if (fillX == true) {
                size.width = canvasSize.width;
                alignX.style = AlignKeys.Center;
            }
            if (fillY == true) {
                size.height = canvasSize.height;
                alignY.style = AlignKeys.Center;
            }
        }
    }

    public enum FillStyle {
        Solid,
        Gradient
    }

    public enum ShapeType {
        Rectangle,
        Circle
    }
}
