using System;
using System.Diagnostics;
using Avalonia.Threading;

namespace EveryTwentyMinutes.Models;

public class Timer
{
    private readonly DispatcherTimer _second = new() { Interval = TimeSpan.FromSeconds(1) };

    public bool IsWorkMode { get; set; } = true; // updates at the start of the next mode
    public enum State
    {
        Idle,
        Running,
        Paused,
        Completed
    }
    public State CurrentState = State.Idle;
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
            _second.Stop();
            CurrentState = State.Completed;
        }
        Tick?.Invoke();
    }

    public void StartWork()
    {
        SecondsRemaining = 5; //20 * 60;
        _second.Start();
        IsWorkMode = true;
        CurrentState = State.Running;
    }

    public void StartBreak()
    {
        SecondsRemaining = 3;
        _second.Start();
        IsWorkMode = false;
        CurrentState = State.Running;
    }

    public void Resume()
    {
        _second.Start();
        CurrentState = State.Running;
    }

    public void Pause()
    {
        _second.Stop();
        CurrentState = State.Paused;
    }
}
