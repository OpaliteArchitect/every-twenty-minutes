using System;
using Avalonia.Threading;

namespace EveryTwentyMinutes.Models;

public class MainModel
{
    public DispatcherTimer MainTimer;
    public DispatcherTimer BreakTimer;

    public MainModel()
    {
        TimeSpan twentyMinutes = new(0, 20, 0);
        MainTimer = new()
        {
            Interval = twentyMinutes
        };
        MainTimer.Tick += MainTimerTick;

        TimeSpan twentySeconds = new(0, 0, 20);
        BreakTimer = new()
        {
            Interval = twentySeconds
        };
        BreakTimer.Tick += BreakTimerTick;
        
    }

    private void MainTimerTick(object? sender, EventArgs e)
    {
        if (sender is DispatcherTimer mainTimer)
        {
            
        }
    }

    private void BreakTimerTick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
