using System;
using SkiaSharp;
namespace SSCreator {
    public class GradientHelper {
        public static SKShader createGradient(SSGradient gradient, SKPoint startPoint, SKPoint endPoint) {
            return SKShader.CreateLinearGradient(
                    startPoint,
                    endPoint,
                    getColors(gradient.colors),
                    getColorOrder(gradient.colors),
                    SKShaderTileMode.Repeat);
        }

        private static SKColor[] getColors(string[] colors) {
            SKColor[] skColors = new SKColor[colors.Length];
            for (int i = 0; i < colors.Length; i++) {
                SKColor.TryParse(colors[i], out SKColor color);
                skColors[i] = color;
            }
            return skColors;
        }

        private static float[] getColorOrder(string[] colors) {
            float[] colorOrder = new float[colors.Length];
            for (int i = 0; i < colors.Length; i++) {
                SKColor.TryParse(colors[i], out SKColor color);
                colorOrder[i] = i;
            }
            return colorOrder;
        }

        public static SKPoint getRectPoint(SKRect rect, SSGradientDirection point) {
            if (point == SSGradientDirection.TopLeft) {
                return new SKPoint(rect.Left, rect.Top);
            } else if (point == SSGradientDirection.TopRight) {
                return new SKPoint(rect.Right, rect.Top);
            } else if (point == SSGradientDirection.BottomLeft) {
                return new SKPoint(rect.Left, rect.Bottom);
            } else {
                return new SKPoint(rect.Right, rect.Bottom);
            }
        }
    }
}
