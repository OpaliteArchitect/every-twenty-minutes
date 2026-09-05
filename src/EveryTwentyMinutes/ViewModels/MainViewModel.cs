using System;
using System.Diagnostics;
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
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_timer.IsWorkMode)
        {
            Description = "20 minutes complete";
        }
        else
        {
            Description = "20 seconds complete";
        }
    }

    [RelayCommand]
    private void ClickButton()
    {
        switch (_timer.CurrentState)
        {
            case Timer.State.Idle:
                if (_timer.IsWorkMode)
                {
                    _timer.StartWork();
                }
                else
                {
                    _timer.StartBreak();
                }
                break;

            case Timer.State.Paused:
                _timer.Resume();
                break;

            case Timer.State.Running:
                _timer.Pause();
                break;

            case Timer.State.Completed:
                _timer.IsWorkMode = !_timer.IsWorkMode;
                _timer.CurrentState = Timer.State.Idle;
                break;
        }
    }
}
