using System;
using SkiaSharp;
namespace SSCreator {
    public class SSShapeGenerator {
        public SSShape[] shapes;
        public SSSize canvasSize;
        public SSShapeGenerator(SSShape[] shapes, SSSize canvasSize) {
            this.shapes = shapes;
            this.canvasSize = canvasSize;
        }

        public void drawShapes(SKCanvas canvas) {
            foreach(SSShape shape in shapes) {
                shape.setSize(canvasSize);
                drawShape(shape, canvas);
            }
        }

        private void drawShape(SSShape shape, SKCanvas canvas) {
            switch(shape.type) {
                case ShapeType.Rectangle:
                    drawRectangle(shape, canvas);
                    break;
                case ShapeType.Circle:
                    drawCircle(shape, canvas);
                    break; 
            }
        }

        private void drawRectangle(SSShape rectangle, SKCanvas canvas) {
            using (var paint = new SKPaint()) {
                paint.IsAntialias = true;

                SKRect rect = new SKRect();
                rect.Size = new SKSize(rectangle.size.width, rectangle.size.height);
                var position = PositionHelper.getPosition(rectangle.alignX, rectangle.alignY, rect.Width, rect.Height, canvasSize);
                rect.Location = new SKPoint(position.x, position.y);
                
                if (rectangle.fillStyle == FillStyle.Solid) {
                    SKColor.TryParse(rectangle.fillColor, out SKColor color);
                    color = color.WithAlpha(rectangle.alpha);
                    paint.Color = color;
                } else if (rectangle.fillStyle == FillStyle.Gradient)  {
                    var startPoint = GradientHelper.getRectPoint(rect, (SSGradientDirection)rectangle.gradient?.startPoint);
                    var endPoint = GradientHelper.getRectPoint(rect, (SSGradientDirection)rectangle.gradient?.endPoint);
                    paint.Shader = GradientHelper.createGradient((SSGradient)rectangle.gradient, startPoint, endPoint);
                } 
                canvas.DrawRect(rect, paint);
            }  
        }

        private void drawCircle(SSShape circle, SKCanvas canvas) {

        }

    }
}
