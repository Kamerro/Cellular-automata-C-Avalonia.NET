using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia;
using Avalonia.Controls;

namespace CoolGITs.Views
{
    public class ImageColoring
    {
        public void ColorTheImage(Image image, uint color)
        {
            var _bitmap = new WriteableBitmap(new PixelSize((int)image.Width, (int)image.Height), new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Premul);

            using (var framebuffer = _bitmap.Lock())
            {
                int bitmapWidth = _bitmap.PixelSize.Width;
                int bitmapHeight = _bitmap.PixelSize.Height;
                unsafe
                {
                    var bufferPtr = (uint*)framebuffer.Address;
                    for (int vertical = 0; vertical < bitmapHeight; vertical++)
                    {
                        for (int horizontal = 0; horizontal < bitmapWidth; horizontal++)
                        {
                            bufferPtr[vertical * bitmapWidth + horizontal] = color;
                        }
                    }
                }
            }
            image.Source = _bitmap;
        }

    }
}