using System;
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
        _timer.Tick += UpdateUI;
    }

    private void UpdateUI()
    {
        int minutes = _timer.SecondsRemaining / 60;
        int seconds = _timer.SecondsRemaining % 60;
        Time 
    }

    [RelayCommand]
    private void ToggleTimer()
    {
        if (_timer.IsRunning)
        {
            _timer.Pause();
        }
        else
        {
            _timer.Resume();
        }
    }
}
