using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace CoolGITs.Views;

public partial class MainWindow
{
    public class ServiceDisplayTheView
    {
        public void DisplayTheView(Image MyImage, RectangleObject[,]rectObj)
        {
            int bitmapWidth = 400;
            int bitmapHeight = 400;
            var _bitmap = new WriteableBitmap(new PixelSize(bitmapWidth, bitmapHeight), new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Premul);

            for (int i = 0; i < (int)(MyImage.Width / Consts.Size); i++)
            {
                for (int j = 0; j < (int)(MyImage.Width / Consts.Size); j++)
                {

                    ChangeChunkOfMap(rectObj[j, i], _bitmap); // 0xFFAACCFF is a color in BGRA format
                }
            }

            MyImage.Source = _bitmap;
        }

        private void ChangeChunkOfMap(RectangleObject obj, WriteableBitmap bitmap)
        {
            using (var framebuffer = bitmap.Lock())
            {
                int bitmapWidth = bitmap.PixelSize.Width;
                int bitmapHeight = bitmap.PixelSize.Height;
                unsafe
                {
                    var bufferPtr = (uint*)framebuffer.Address;
                    for (int vertical = (int)obj.Position.Y; vertical < (int)obj.Position.Y + obj.Size && vertical < bitmapHeight; vertical++)
                    {
                        for (int horizontal = (int)obj.Position.X; horizontal < (int)obj.Position.X + obj.Size && horizontal < bitmapWidth; horizontal++)
                        {
                            bufferPtr[vertical * bitmapWidth + horizontal] = obj.ColorInHexa;
                        }
                    }
                }
            }
        }

     
    }
}
