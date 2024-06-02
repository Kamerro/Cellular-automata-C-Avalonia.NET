using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.Threading;

namespace CoolGITs.Views;

public partial class MainWindow : Window
{
    private RectangleObject[,] _rectObj;
    private RectangleService _service = new RectangleService();
    private ServiceDisplayTheView _display = new ServiceDisplayTheView();
    private ImageColoring _ic = new ImageColoring();
    private DispatcherTimer _dt = new DispatcherTimer();

    public MainWindow()
    {
        InitializeComponent();
        InitializeGame();
        _dt.Tick += Run;
        _dt.IsEnabled = true;
        _dt.Interval = new TimeSpan(0, 0, 0, 0, 200);   
    }

    private void Run(object? sender, EventArgs e)
    {
        int rows = _rectObj.GetLength(0);
        int cols = _rectObj.GetLength(1);
        RectangleObject[,] newRectObj = new RectangleObject[cols, rows];

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int liveNeighbors = CountLiveNeighbors(i, j, rows, cols);

                newRectObj[j, i] = new RectangleObject
                {
                    Position = _rectObj[j, i].Position,
                    Size = _rectObj[j, i].Size,
                    ColorInHexa = DetermineNextState(_rectObj[j, i].ColorInHexa, liveNeighbors)
                };
            }
        }

        _rectObj = newRectObj;
        DisplayMap();
    }

    private int CountLiveNeighbors(int x, int y, int rows, int cols)
    {
        int liveNeighbors = 0;

        for (int ii = x - 1; ii <= x + 1; ii++)
        {
            for (int jj = y - 1; jj <= y + 1; jj++)
            {
                if (IsInBounds(ii, jj, rows, cols) && !(ii == x && jj == y))
                {
                    if (_rectObj[jj, ii].ColorInHexa != 0xFFFFFFFF)
                    {
                        liveNeighbors++;
                    }
                }
            }
        }

        return liveNeighbors;
    }

    private bool IsInBounds(int x, int y, int rows, int cols)
    {
        return x >= 0 && x < cols && y >= 0 && y < rows;
    }

    private uint DetermineNextState(uint currentColor, int liveNeighbors)
    {
        if (currentColor == 0xFF0000FF)
        {
            if (liveNeighbors == 2 || liveNeighbors == 3)
            {
                return 0xFF0000FF;
            }
            else
            {
                return 0xFFFFFFFF;
            }
        }
        else
        {
            if (liveNeighbors == 3)
            {
                return 0xFF0000FF;
            }
            else
            {
                return 0xFFFFFFFF;
            }
        }
    }

    private void InitializeGame()
    {
        CreateNewRectangleObject();
        _service.CreateFullListOfRectangles(_rectObj, Consts.Size, MyImage);
        DisplayMap();
        AddMethodsToPointerPressedEvent();
        ColorTheImages();
    }

    private void ColorTheImages()
    {

        _ic.ColorTheImage(BlueColor, 0xFF0000FF);
        _ic.ColorTheImage(RedColor, 0xFFFF0000);
        _ic.ColorTheImage(GreenColor, 0xFF00FF00);
        _ic.ColorTheImage(BlackColor, 0xFF000000);
        _ic.ColorTheImage(WhiteColor, 0xFFFFFFFF);
        _ic.ColorTheImage(PurpleColor, 0xFFA020F0);
        _ic.ColorTheImage(BrownColor, 0xFFA52A2A);
    }

    private void AddMethodsToPointerPressedEvent()
    {
        MyImage.PointerPressed += ChangeParamaterOfRectangle;
        BlueColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfClickingBlue;
        RedColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingRed;
        GreenColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingGreen;
        BlackColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingBlack;
        WhiteColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingWhite;
        PurpleColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingPurple;
        BrownColor.PointerPressed += ChangeColorsEventHandlers.changeColorOfCLickingBrown;
    }

    private void ChangeStateOfTheGame(object sender, RoutedEventArgs args)
    {
        
        var obj = sender as Button;
        if(Consts.StateOfTheGame == StateOfTheGame.Normal)
        {
            Consts.StateOfTheGame = StateOfTheGame.Freezed;
            obj.Content = "Start the game!";
            _dt.Stop();
        }
        else
        {
            Consts.StateOfTheGame = StateOfTheGame.Normal;
            obj.Content = "Stop the game!";
            _dt.Start();

        }
    }
    private void ChangeParamaterOfRectangle(object? sender, PointerEventArgs e)
    {
       
            Avalonia.Point point = e.GetPosition(sender as Image);
            foreach (var obj in _rectObj)
            {
                if (obj.Position.X < point.X && point.X < obj.Position.X + Consts.Size && obj.Position.Y < point.Y && point.Y < obj.Position.Y + Consts.Size)
                {
                    obj.ColorInHexa = Consts.ColorOfClicks;
                }
            }

            DisplayMap();
        
    }

    private void DisplayMap()
    {
        _display.DisplayTheView(MyImage, _rectObj);
    }

    private void CreateNewRectangleObject()
    {
        _rectObj = new RectangleObject[(int)(MyImage.Width / Consts.Size),
                                     (int)(MyImage.Height / Consts.Size)];
    }
}
