using Avalonia;
using Avalonia.Controls;

namespace CoolGITs.Views;

public partial class MainWindow
{
    public class RectangleService
    {
        public void CreateFullListOfRectangles(RectangleObject[,] rectObj, int size,Image MyImage)
        {
            for (int i = 0; i < (int)(MyImage.Height / Consts.Size); i++)
            {
                for (int j = 0; j < (int)(MyImage.Width / Consts.Size); j++)
                {
                    rectObj[j, i] = new RectangleObject();
                    rectObj[j, i].ColorInHexa = 0xFFFFFFFF;
                    rectObj[j, i].Position = new Point(j * size, i * size);
                    rectObj[j, i].Size = size;

                }
            }
        }

    }
}
