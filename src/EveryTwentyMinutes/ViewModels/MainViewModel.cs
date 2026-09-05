using System;
using System.Diagnostics;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EveryTwentyMinutes.Models;

namespace EveryTwentyMinutes.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Time { get; set; } = "20:00";
    [ObservableProperty]
    public partial string Description { get; set; } = "Next break in";
    [ObservableProperty]
    public partial string ButtonText { get; set; } = "Start timer";

    private readonly Timer _timer = new();

    public MainViewModel()
    {
        _timer.Tick += OnSecondElapsed;
    }

    private void OnSecondElapsed()
    {
        Debug.WriteLine("elapsed");
        int minutes = _timer.SecondsRemaining / 60;
        int seconds = _timer.SecondsRemaining % 60;

        Time = $"{minutes:d2}:{seconds:d2}";

        if (_timer.SecondsRemaining is 0)
        {
            ShowCompletedUI();
        }
    }

    private void ShowCompletedUI()
    {
        if (_timer.IsWorkMode)
        {
            Description = "Time for a break";
            Time = "Look at something 20 feet away for 20 seconds.";
            ButtonText = "Start looking";
        }
        else
        {
            Description = "Great job!";
            Time = "Your 20 second break is complete. Confirm to resume the 20 minute interval timer.";
            ButtonText = "Confirm";
        }
    }

    [RelayCommand]
    private void ClickButton()
    {
        switch (_timer.CurrentState)
        {
            case Timer.State.Idle when _timer.IsWorkMode:
                _timer.StartWork();
                Time = "20:00";
                ButtonText = "Pause timer";
                break;

            case Timer.State.Idle when !_timer.IsWorkMode:
                _timer.StartBreak();
                Description = "Look 20 feet away";
                Time = "00:20";
                ButtonText = "Pause timer";
                break;

            case Timer.State.Paused:
                _timer.Resume();
                ButtonText = "Pause timer";
                break;

            case Timer.State.Running:
                _timer.Pause();
                ButtonText = "Resume timer";
                break;

            case Timer.State.Completed:
                _timer.IsWorkMode = !_timer.IsWorkMode;
                _timer.CurrentState = Timer.State.Idle;
                if (_timer.IsWorkMode)
                {
                    Description = "Next break in";
                    Time = "20:00";
                    ButtonText = "Start timer";
                }
                else
                {
                    ClickButton();
                }
                break;
        }
    }
}
