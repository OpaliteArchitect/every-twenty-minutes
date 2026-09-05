using System;
using Avalonia.Threading;

namespace EveryTwentyMinutes.Models;

public class Timer
{
    private readonly DispatcherTimer _second = new() { Interval = TimeSpan.FromSeconds(1) };

    public bool IsWorkMode { get; set; } // updates at the start of the next mode
    public bool IsRunning => _second.IsEnabled;
    public int SecondsRemaining { get; set; }
    public event Action? Tick;

    public Timer()
    {
        _second.Tick += OnSecondTick;
    }

    private void OnSecondTick(object? sender, EventArgs e)
    {
        SecondsRemaining--;

        if (SecondsRemaining is 0)
        {
            Tick?.Invoke();
            _second.Stop();
        }
    }

    public void StartWork()
    {
        SecondsRemaining = 20 * 60;
        _second.Start();
        IsWorkMode = true;
    }

    public void StartBreak()
    {
        SecondsRemaining = 20;
        _second.Start();
        IsWorkMode = false;
    }

    public void Resume() => _second.Start();
    
    public void Pause() => _second.Stop();
}
