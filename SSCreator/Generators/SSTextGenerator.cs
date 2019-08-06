using System;
using SkiaSharp;
namespace SSCreator {
    public class SSTextGenerator {
        private SSText[] texts;
        private SSSize canvasSize;

        public SSTextGenerator(SSText[] texts, SSSize canvasSize) {
            this.texts = texts;
            this.canvasSize = canvasSize;
        }

        public void drawTexts(SKCanvas canvas) {
            foreach (SSText text in texts) {
                drawText(text, canvas);
            }
        }

        private void drawText(SSText text, SKCanvas canvas) {
            using (var paint = new SKPaint()) {
                paint.TextSize = text.fontSize;
                paint.IsAntialias = true;
                paint.Typeface = getFont(text.fontName);
                SKColor.TryParse(text.color, out SKColor color);
                paint.Color = color;
                var height = text.fontSize;
                var width = paint.MeasureText(text.text);
                var position = PositionHelper.getPosition(text.alignX, text.alignY, width, height, canvasSize);
                position.y += text.fontSize;
                if (text.rotation.HasValue && text.rotation > 0) {
                    using (SKPath path = new SKPath()) {
                        path.MoveTo(width / 10, height - height / 10);
                        path.LineTo(width - width / 10, height / 10);
                        canvas.DrawTextOnPath(text.text, path, 0, 0, paint);
                    }
 
                } else {
                    canvas.DrawText(text.text, position.x, position.y, paint);
                }
            }
        }

        private SKTypeface getFont(string fontName) {
            try {
                if (fontName.Contains(".ttf")) {
                    return SKTypeface.FromFile(fontName);
                } else {
                    return SKTypeface.FromFamilyName(fontName);
                }
            } catch(Exception ex) {
                Print.Error(ex.Message);
                return SKTypeface.FromFamilyName("Roboto");
            }
        }
    }
}
