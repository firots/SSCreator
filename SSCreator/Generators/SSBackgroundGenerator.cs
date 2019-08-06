﻿using System;
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
            if (background.type == SSBackgroundType.solid) {
                drawSolidBackground(canvas);
            } else if (background.type == SSBackgroundType.image) {
                drawImageBackground(canvas);
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
    }
}
