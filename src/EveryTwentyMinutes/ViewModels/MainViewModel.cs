using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EveryTwentyMinutes.Models;
using EveryTwentyMinutes.Services;

namespace EveryTwentyMinutes.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private const int _timeFontSize = 64;
    private const int _textFontSize = 24;
    private const string WorkDurationText = "00:05";
    private const string BreakDurationText = "00:03";

    [ObservableProperty]
    public partial string MainText { get; set; } = WorkDurationText;
    [ObservableProperty]
    public partial int MainTextFontSize { get; set; } = _timeFontSize;
    [ObservableProperty]
    public partial string Description { get; set; } = "Next break in";
    [ObservableProperty]
    public partial string ButtonText { get; set; } = "Start timer";
    [ObservableProperty]
    public partial string ButtonColor { get; set; } = "Green";

    private readonly Timer _timer = new();
    private readonly WindowsAudioService _audioService = new();

    public MainViewModel()
    {
        _timer.Tick += OnSecondElapsed;
    }

    private void OnSecondElapsed()
    {
        int minutes = _timer.SecondsRemaining / 60;
        int seconds = _timer.SecondsRemaining % 60;

        MainText = $"{minutes:d2}:{seconds:d2}";

        if (_timer.SecondsRemaining is 0)
        {
            ShowCompletedUI();
        }
    }

    private async Task ShowCompletedUI()
    {
        if (_timer.IsWorkMode)
        {
            WorkCompletedUIUpdate();
            _ = _audioService.PlayDoubleBeep();
        }
        else
        {
            BreakCompletedUIUpdate();
            _ = _audioService.PlaySingleBeep();
        }
    }

    private void WorkCompletedUIUpdate()
    {
        Description = "Time for a break";
        MainText = "Look at something 20 feet away for 20 seconds.";
        MainTextFontSize = _textFontSize;
        ButtonText = "Start looking";
        ButtonColor = "Green";
    }

    private void BreakCompletedUIUpdate()
    {
        Description = "Great job!";
        MainText = "Your 20 second break is complete.";
        MainTextFontSize = _textFontSize;
        ButtonText = "Confirm";
        ButtonColor = "Green";
    }

    [RelayCommand]
    private void ClickButton()
    {
        switch (_timer.CurrentState)
        {
            case Timer.State.Idle when _timer.IsWorkMode:
                StartWorkUIUpdate();
                break;

            case Timer.State.Idle when !_timer.IsWorkMode:
                StartBreakUIUpdate();
                break;

            case Timer.State.Paused:
                _timer.Resume();
                ButtonText = "Pause timer";
                ButtonColor = "Purple";
                break;

            case Timer.State.Running:
                _timer.Pause();
                ButtonText = "Resume timer";
                ButtonColor = "Green";
                break;

            case Timer.State.Completed:
                EndCurrentWorkMode();
                break;
        }
    }

    private void EndCurrentWorkMode()
    {
        _timer.IsWorkMode = !_timer.IsWorkMode;
        _timer.CurrentState = Timer.State.Idle;
        if (_timer.IsWorkMode)
        {
            Description = "Next break in";
            MainText = WorkDurationText;
            MainTextFontSize = _timeFontSize;
            ButtonText = "Start timer";
            ButtonColor = "Green";
        }
        else
        {
            ClickButton();
        }
    }

    private void StartBreakUIUpdate()
    {
        _timer.StartBreak();
        Description = "Look 20 feet away";
        MainText = BreakDurationText;
        MainTextFontSize = _timeFontSize;
        ButtonText = "Pause timer";
        ButtonColor = "Purple";
    }

    private void StartWorkUIUpdate()
    {
        _timer.StartWork();
        MainText = WorkDurationText;
        MainTextFontSize = _timeFontSize;
        ButtonText = "Pause timer";
        ButtonColor = "Purple";
    }
}
