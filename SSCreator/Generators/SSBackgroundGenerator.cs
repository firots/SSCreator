using System;
using SkiaSharp;
namespace SSCreator {
    public class SSBackgroundGenerator {
        private SSBackground background;
        private SSSize canvasSize;
        public SSBackgroundGenerator(SSBackground background, SSSize canvasSize) {
            this.background = background;
            this.canvasSize = canvasSize;
        }

        public void drawBackground(SKCanvas canvas) {
            if (background.type == SSBackgroundType.Solid) {
                drawSolidBackground(canvas);
            } else if (background.type == SSBackgroundType.Image) {
                drawImageBackground(canvas);
            } else if (background.type == SSBackgroundType.Gradient) {
                drawGradientBackground(canvas);
            }
        }

        private void drawImageBackground(SKCanvas canvas) {
            SKBitmap bgBitmap = SKBitmap.Decode(background.imagePath);
            if (bgBitmap.Width != canvasSize.width || bgBitmap.Height != canvasSize.height) {
                var info = new SKImageInfo(canvasSize.width, canvasSize.height);
                bgBitmap = bgBitmap.Resize(info, SKFilterQuality.High);
            }
            if (background.blur.HasValue && background.blur != 0) {
                var filter = SKImageFilter.CreateBlur((int)background.blur, (int)background.blur);
                var paint = new SKPaint {
                    ImageFilter = filter
                };
                canvas.DrawBitmap(bgBitmap, new SKPoint(0, 0), paint);
            } else {
                canvas.DrawBitmap(bgBitmap, new SKPoint(0, 0));
            }
        }

        private void drawSolidBackground(SKCanvas canvas) {
            SKColor.TryParse(background.color, out SKColor color);
            canvas.Clear(color);
        }

        private void drawGradientBackground(SKCanvas canvas) {
            SSShape gradientBg = new SSShape {
                size = canvasSize,
                alignX = new SSAlign(),
                alignY = new SSAlign(),
                fillX = true,
                fillY = true,
                type = ShapeType.Rectangle,
                fillStyle = FillStyle.Gradient,
                gradient = background.gradient
            };
            gradientBg.setSize(canvasSize);
            SSShapeGenerator shapeGenerator = new SSShapeGenerator(new SSShape[] { gradientBg }, canvasSize);
            shapeGenerator.drawShapes(canvas);
        }
    }
}
