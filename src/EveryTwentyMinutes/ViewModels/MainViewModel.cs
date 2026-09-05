using CommunityToolkit.Mvvm.ComponentModel;

namespace EveryTwentyMinutes.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Timer { get; set; } = "20:00";
    [ObservableProperty]
    public partial string Description { get; set; } = "Next break in";
    [ObservableProperty]
    public partial string ButtonText { get; set; } = "";


}
