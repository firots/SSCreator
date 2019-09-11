using System;
using SkiaSharp;
namespace SSCreator {
    public class SSBackgroundGenerator: IDisposable {
        private SSBackground background;
        private SSSize canvasSize;
        public SSBackgroundGenerator(SSBackground background, SSSize canvasSize) {
            this.background = background;
            this.canvasSize = canvasSize;
        }

        public void drawBackground(SKCanvas canvas) {
            switch (background.type) {
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

        public void drawAdaptiveBackground(SKCanvas canvas, SKBitmap bitmap) {
            if (background.type != SSBackgroundType.Adaptive) return;
            if (bitmap.Width != canvasSize.width || bitmap.Height != canvasSize.height) {
                Print.Warning("Adaptive background size is not compatible with screenshot size, resizing adaptive background...");
                var info = new SKImageInfo(canvasSize.width, canvasSize.height);
                bitmap = bitmap.Resize(info, SKFilterQuality.High);
            }
            var filter = SKImageFilter.CreateBlur(20, 20);
            var paint = new SKPaint {
                ImageFilter = filter
            };
            canvas.DrawBitmap(bitmap, new SKPoint(0, 0), paint);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SSBackgroundGenerator()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
