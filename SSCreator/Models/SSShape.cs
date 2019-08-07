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
        public SSAlign alignX;
        public SSAlign alignY;
        public int? diameter;
        public SSSize size;
        [JsonConverter(typeof(StringEnumConverter))]
        public Stretch? stretch;


        public void setSize(SSSize canvasSize) {
            if (stretch != null) {
                if (stretch == Stretch.X) {
                    size.width = canvasSize.width;
                    alignX.style = AlignKeys.Center;
                } else {
                    size.height = canvasSize.height;
                    alignY.style = AlignKeys.Center;
                }
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

    public enum Stretch {
        X,
        Y
    }
}
