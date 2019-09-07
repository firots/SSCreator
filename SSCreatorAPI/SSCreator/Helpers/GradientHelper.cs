using System;
using SkiaSharp;
namespace SSCreator {
    public class GradientHelper {
        public static SKShader createGradient(SSGradient gradient, SKPoint startPoint, SKPoint endPoint) {
            return SKShader.CreateLinearGradient(
                    startPoint,
                    endPoint,
                    getColors(gradient.colors),
                    null,
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

        public static SKPoint getRectPoint(SKRect rect, SSGradientDirection point) {
            switch (point)
            {
                case SSGradientDirection.TopLeft:
                    return new SKPoint(rect.Left, rect.Top);
                case SSGradientDirection.TopRight:
                    return new SKPoint(rect.Right, rect.Top);
                case SSGradientDirection.BottomLeft:
                    return new SKPoint(rect.Left, rect.Bottom);
                default:
                    return new SKPoint(rect.Right, rect.Bottom);
            }
        }
    }
}
