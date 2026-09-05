using CommunityToolkit.Mvvm.ComponentModel;

namespace EveryTwentyMinutes.ViewModels;

public partial class MainTimerViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string MainTimer { get; set; } = "20:00";
}
