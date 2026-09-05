using System;
using Avalonia.Threading;

namespace EveryTwentyMinutes.Models;

public class MainTimerModel
{
    public DispatcherTimer MainTimer;

    public MainTimerModel()
    {
        TimeSpan twentyMinutes = new(0, 20, 0);
        MainTimer = new()
        {
            Interval = twentyMinutes
        };
        MainTimer.Tick += MainTimerTick;
    }

    private void MainTimerTick(object? sender, EventArgs e)
    {
        if (sender is DispatcherTimer mainTimer)
        {
            
        }
    }
}
