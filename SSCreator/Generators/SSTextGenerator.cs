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
            var lineNum = 0;
            float textheight = getTextHeight(text);
            SSPosition startPoint = PositionHelper.getPosition(text.alignX, text.alignY, 0, textheight, canvasSize);
            SSPosition position = startPoint;
            foreach (SSLine line in text.lines) {
                LineProps lineProps = calculateLineProps(line);
                if (text.alignX.style == AlignKeys.Center) {
                    position.x -= (lineProps.width / 2);
                } else if (text.alignX.style == AlignKeys.Right) {
                    position.x -= lineProps.width;
                }
                if (lineNum == 0) {
                    position.y += lineProps.maxHeight;
                }
                position.y += lineProps.maxHeight * lineNum;
                drawLine(line, lineNum, position, canvas);
                lineNum++;
                position.x = startPoint.x;
            }
        }

        private float getTextHeight(SSText text) {
            float height = 0;
            foreach (SSLine line in text.lines) {
                height += calculateLineProps(line).maxHeight;
            }
            return height;
        }

        private void drawLine(SSLine line, int lineNum, SSPosition position, SKCanvas canvas) {
            foreach (SSLabel label in line.labels) {
                position = drawLabel(label, lineNum, canvas, position);
            }
        }

        private SSPosition drawLabel(SSLabel label, int lineNum, SKCanvas canvas, SSPosition position) {
            using (SKPaint paint = new SKPaint()) {
                Print.Warning(label.text + position.x + position.y);
                paint.TextSize = label.fontSize;
                paint.IsAntialias = true;
                paint.Typeface = getFont(label.fontName);
                SKColor.TryParse(label.color, out SKColor color);
                paint.Color = color;
                float height = label.fontSize;
                canvas.DrawText(label.text, position.x, position.y, paint);
                position.x += paint.MeasureText(label.text);
                return position;
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

        private LineProps calculateLineProps(SSLine line) {
            LineProps lineProps = new LineProps(0, 0);
            foreach (SSLabel label in line.labels) {
                using (SKPaint paint = new SKPaint()) {
                    paint.TextSize = label.fontSize;
                    paint.IsAntialias = true;
                    paint.Typeface = getFont(label.fontName);
                    lineProps.width += paint.MeasureText(label.text);
                    if (label.fontSize > lineProps.maxHeight) {
                        lineProps.maxHeight = label.fontSize;
                    }
                }
            }
            return lineProps;
        }
    }

    public struct LineProps {
        public float maxHeight;
        public float width;

        public LineProps(float maxHeight, float width) {
            this.maxHeight = maxHeight;
            this.width = width;
        }
    }
}
