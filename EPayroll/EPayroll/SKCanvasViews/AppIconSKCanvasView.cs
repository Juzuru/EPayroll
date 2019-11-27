using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace EPayroll.SKCanvasViews
{
    public class AppIconSKCanvasView : SKCanvasView
    {
        public AppIconSKCanvasView()
        {
            WidthRequest = 140;
            HeightRequest = 280 / Math.Sqrt(3);
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            float f = e.Info.Width / 2 / (float)Math.Sqrt(3);

            double height = DeviceDisplay.MainDisplayInfo.Height;
            double width = DeviceDisplay.MainDisplayInfo.Width;

            SKPath path = new SKPath();
            path.MoveTo(e.Info.Width / 2, 0);
            path.LineTo(e.Info.Width, f);
            path.LineTo(e.Info.Width, f * 3);
            path.LineTo(e.Info.Width / 2, f * 4);
            path.LineTo(0, f * 3);
            path.LineTo(0, f);
            path.Close();

            canvas.DrawPath(path, new SKPaint { 
                Style = SKPaintStyle.Fill,
                Color = new SKColor(69, 190, 171)
            });

            //path = new SKPath();
            //path.MoveTo(e.Info.Width / 3, e.Info.Width / 4 * 3);
            //path.LineTo(e.Info.Width / 3, e.Info.Width / 4);
            //path.LineTo(e.Info.Width / 2, e.Info.Width / 4);
            //path.CubicTo(e.Info.Width / 3 * 2, e.Info.Width / 4, 
             //   e.Info.Width / 3 * 2, e.Info.Width / 4,
              //  e.Info.Width / 2, e.Info.Width / 4);
            //path.Close();

//            canvas.DrawPath(path, new SKPaint { 
 //               Style = SKPaintStyle.Stroke,
   //             Color = SKColors.White
     //       });
        }
    }
}
