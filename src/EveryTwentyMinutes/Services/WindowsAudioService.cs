using System;
using System.Threading.Tasks;

namespace EveryTwentyMinutes.Services;

public class WindowsAudioService
{
    public async Task PlayDoubleBeep()
    {
        for (int i = 0; i < 5; i++)
        {
            await Task.Run(() => Console.Beep(800, 1000));
            await Task.Delay(750);
        }
    }

    public async Task PlaySingleBeep()
    {
        for (int i = 0; i < 3; i++)
        {
            await Task.Run(() => Console.Beep(1200, 500));
            await Task.Delay(250);
        }
    }
}
