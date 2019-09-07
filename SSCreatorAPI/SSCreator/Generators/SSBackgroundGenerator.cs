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
            switch (background.type)
            {
                case SSBackgroundType.Solid:
                    drawSolidBackground(canvas);
                    break;
                case SSBackgroundType.Image:
                    drawImageBackground(canvas);
                    break;
                case SSBackgroundType.Gradient:
                    drawGradientBackground(canvas);
                    break;
            }
        }

        private void drawImageBackground(SKCanvas canvas) {
            SKBitmap bgBitmap = SkiaHelper.createPersistentBitmap(background.imagePath, canvasSize.width, canvasSize.height);
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
