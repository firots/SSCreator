using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SSCreator {
    public class PositionHelper {
        public static SSPosition getPosition(SSAlign alignX, SSAlign alignY, float width, float height, SSSize canvasSize) {
            var calculatedPos = new SSPosition(0, 0);
            if (alignX != null) {
                if (alignX.style == AlignKeys.Center) {
                    calculatedPos.x = (canvasSize.width - width) / 2;
                } else if (alignX.style == AlignKeys.Left) {
                    calculatedPos.x = alignX.value;
                } else if (alignX.style == AlignKeys.Right) {
                    calculatedPos.x = canvasSize.width - width - alignX.value;
                }
            }
            if (alignY != null) {
                if (alignY.style == AlignKeys.Center) {
                    calculatedPos.y = (canvasSize.height - height) / 2;
                } else if (alignY.style == AlignKeys.Top) {
                    calculatedPos.y = alignY.value;
                } else if (alignY.style == AlignKeys.Bottom) {
                    calculatedPos.y = canvasSize.height - height - alignY.value;
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
        public int value = 0;
    }
}
