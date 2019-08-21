using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class PositionHelper {
        public static SSPosition getPosition(SSAlign alignX, SSAlign alignY, float width, float height, SSSize canvasSize) {
            var calculatedPos = new SSPosition(0, 0);
            if (alignX != null) {
                switch (alignX.style)
                {
                    case AlignKeys.Center:
                        calculatedPos.x = ((canvasSize.width - width) / 2) + alignX.value;
                        break;
                    case AlignKeys.Left:
                        calculatedPos.x = alignX.value;
                        break;
                    case AlignKeys.Right:
                        calculatedPos.x = canvasSize.width - width - alignX.value;
                        break;
                }
            }
            if (alignY != null) {
                switch (alignY.style)
                {
                    case AlignKeys.Center:
                        calculatedPos.y = (canvasSize.height - height) / 2 + alignY.value;
                        break;
                    case AlignKeys.Top:
                        calculatedPos.y = alignY.value;
                        break;
                    case AlignKeys.Bottom:
                        calculatedPos.y = canvasSize.height - height - alignY.value;
                        break;
                }
            }
            return calculatedPos;
        }
    }

    public enum AlignKeys {
        Left,
        Right,
        Top,
        Bottom,
        Center
    }

    public class SSAlign {
        [JsonConverter(typeof(StringEnumConverter))]
        public AlignKeys style = AlignKeys.Center;
        public int value;
    }
}
