using Avalonia.Input;
using static CoolGITs.Views.MainWindow;

namespace CoolGITs.Views
{
    public class ChangeColorsEventHandlers
    {
        public static void changeColorOfCLickingBrown(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFFA52A2A;
        }

        public static void changeColorOfCLickingPurple(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFFA020F0;
        }

        public static void changeColorOfCLickingWhite(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFFFFFFFF;
        }

        public static void changeColorOfCLickingBlack(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFF000000;
        }

        public static void changeColorOfCLickingGreen(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFF008000;
        }

        public static void changeColorOfCLickingRed(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFFFF0000;
        }

        public static void changeColorOfClickingBlue(object? sender, PointerPressedEventArgs e)
        {
            Consts.ColorOfClicks = 0xFF0000FF;
        }

    }
}