using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace CriptoPortfolio1.Classes
{
    class Draww
    {
        public int xt = 0;
        public int yt = 0;

        public Draww(int xt, int yt)
        {
            this.xt = xt;
            this.yt = yt;
        }

        SKPaint paint = new SKPaint  // свойство кисти 
        {
            Style = SKPaintStyle.Stroke,
            Color = Color.Red.ToSKColor(),
            StrokeWidth = 50
        };



        int width = 0;
        int height = 0;

        int i = 0;

        public List<Bar_Class> Bar = new List<Bar_Class>();

        public void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            width = e.Info.Width;
            height = e.Info.Height;
            canvas.Translate(xt, yt); // начало отрисовки

            // canvas.Scale(0.5f); // Масштаб

            canvas.Scale(1, 1); // Масштаб


            draweng(canvas);

            i++;

        }

        void draweng(SKCanvas canvas)
        {

            canvas.Clear(SKColors.Lime);
            canvas.Save();

            if (Bar.Count > 0)
            {

                int size = (width / Bar.Count);
                int j = -size / 2;

                double max = Bar[0].low;
                double min = Bar[0].high;
                float y = 0;
                float y1 = 0;
                float y2 = 0;

                #region // отрисовка баров

                for (int i = 0; i < Bar.Count; i++)
                {
                    if (Bar[i].high > max && Bar[i].close != 0) { max = Bar[i].high; }
                    if (Bar[i].low < min && Bar[i].close != 0) { min = Bar[i].low; }
                }


                if (max == min) return;

                for (int i = 0; i < Bar.Count; i++)
                {
                    if (Bar[i].close == 0 || min == max) { continue; }

                    j = j + size - 3;


                    if (Bar[i].FF() == 1)
                    {
                        Bar[i].point_Price(ref y, ref y1, ref y2, height, max, min);

                        paint.Color = Color.Green.ToSKColor();
                        paint.StrokeWidth = size - 6;
                        canvas.DrawLine(j, y, j, y1, paint);

                        if (Bar.Count - 1 == i) // тик цены на графеке
                        {

                            paint.Color = Color.Black.ToSKColor();
                            paint.StrokeWidth = 5;
                            canvas.DrawLine(j + (size - 6), y2, width, y2, paint);
                        }

                    }

                    if (Bar[i].FF() == 2)
                    {

                        Bar[i].point_Price(ref y, ref y1, ref y2, height, max, min);

                        paint.Color = Color.Red.ToSKColor();
                        paint.StrokeWidth = size - 6;
                        canvas.DrawLine(j, y, j, y1, paint);

                        if (Bar.Count - 1 == i) // тик цены на графеке
                        {
                            paint.Color = Color.Black.ToSKColor();
                            paint.StrokeWidth = 5;
                            canvas.DrawLine(j + (size - 6), y2, width, y2, paint);

                        }
                    }

                    if (Bar[i].FF() == 3)
                    {
                        Bar[i].point_Price(ref y, ref y1, ref y2, height, max, min);

                        paint.Color = Color.Yellow.ToSKColor();
                        paint.StrokeWidth = size - 6;
                        canvas.DrawLine(j, y, j, y1 + 3, paint);

                        if (Bar.Count - 1 == i) // тик цены на графеке
                        {
                            paint.Color = Color.Black.ToSKColor();
                            paint.StrokeWidth = 5;
                            canvas.DrawLine(j + (size - 6), y2, width, y2, paint);

                        }
                    }


                }

                #endregion


            }

            #region // рамка
            paint.Color = Color.Black.ToSKColor();
            paint.StrokeWidth = 6;
            canvas.DrawLine(0, 0, 0, height, paint);
            canvas.DrawLine(0, 0, width, 0, paint);
            canvas.DrawLine(0, height, width, height, paint);
            canvas.DrawLine(width, 0, width, height, paint);
            #endregion

            canvas.Restore();
        }



    }
}
