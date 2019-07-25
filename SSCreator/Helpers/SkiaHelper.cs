using System;
using System.IO;
using SkiaSharp;


namespace SSCreator {
    public static class SkiaHelper {
        public static SKBitmap scaleBitmap(SKBitmap bitmap, double ratio) {
            var newHeight = Convert.ToInt32(bitmap.Height * ratio);
            var newWidth = Convert.ToInt32(bitmap.Width * ratio);
            var info = new SKImageInfo(newWidth, newHeight);
            return bitmap.Resize(info, SKFilterQuality.High);
        }

        public static void saveBitmap(SKBitmap bitmap, string path) {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                SKData data = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
                var bytes = data.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static SKBitmap rotateBitmap(SKBitmap bitmap, double angle) {
            double radians = Math.PI * angle / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = bitmap.Width;
            int originalHeight = bitmap.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

            using (SKCanvas canvas = new SKCanvas(rotatedBitmap)) {
                canvas.Translate(rotatedWidth / 2, rotatedHeight / 2);
                canvas.RotateDegrees((float)angle);
                canvas.Translate(-originalWidth / 2, -originalHeight / 2);
                canvas.DrawBitmap(bitmap, new SKPoint());
            }
            return rotatedBitmap;
        }

        public static SKBitmap overlayBitmaps(Tuple<SKBitmap, SKPoint>[] bitmaps) {
            SKBitmap firstBitmap = bitmaps[0].Item1;
            SKBitmap foundation = new SKBitmap(firstBitmap.Width, firstBitmap.Height);
            using (SKCanvas tempCanvas = new SKCanvas(foundation)) {
                for (int i = bitmaps.Length - 1; i > -1; i--) {
                    tempCanvas.DrawBitmap(bitmaps[i].Item1, bitmaps[i].Item2);
                }
            }
            return foundation;
        }
    }
}
