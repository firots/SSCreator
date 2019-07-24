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
    }
}
