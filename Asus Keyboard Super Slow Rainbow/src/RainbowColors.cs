using System.Drawing;

namespace Asus_Keyboard_Super_Slow_Rainbow;

public class RainbowColors
{
    private readonly int _step;

    private readonly int _iterations;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly TimeSpan _desiredDuration;
    private readonly TimeSpan _sleepBetweenIterations;

    public readonly CancellationTokenSource Cts;

    public RainbowColors(TimeSpan desiredDuration, int step = 5)
    {
        _step = step;
        _iterations = 256 * 6 / _step;
        _desiredDuration = desiredDuration;
        _sleepBetweenIterations = desiredDuration / _iterations;
        Cts = new CancellationTokenSource();

        Console.WriteLine($"""
            Number of colors changed in 1 rainbow cycle: {_iterations}.
            Desired Duration: {_desiredDuration}.
            Sleep between each color change: {_sleepBetweenIterations}.
            """);
    }


    // ReSharper disable once FunctionNeverReturns
    public void DoTheRainbow()
    {
        while (true)
        {
            if (Cts.IsCancellationRequested) return;

            // ### Red ➡️ Orange ➡️ Yellow
            Console.WriteLine("Red ➡️ Orange ➡️ Yellow");
            for (int green = 0; green <= 255; green += _step) SetRgb(255, green, 0);

            // ### Yellow ➡️ Green
            Console.WriteLine("Yellow ➡️ Green");
            for (int red = 255; red >= 0; red -= _step) SetRgb(red, 255, 0);

            // ### Green ➡️ Cyan
            Console.WriteLine("Green ➡️ Cyan");
            for (int blue = 0; blue <= 255; blue += _step) SetRgb(0, 255, blue);

            // ### Cyan ➡️ Blue
            Console.WriteLine("Cyan ➡️ Blue");
            for (int green = 255; green >= 0; green -= _step) SetRgb(0, green, 255);

            // ### Blue ➡️ Purple
            Console.WriteLine("Blue ➡️ Purple");
            for (int red = 0; red <= 255; red += _step) SetRgb(red, 0, 255);

            // ### Purple ➡️ Red
            Console.WriteLine("Purple ➡️ Red");
            for (int blue = 255; blue >= 0; blue -= _step) SetRgb(255, 0, blue);

            Console.WriteLine("============== Restart");
        }
    }


    private void SetRgb(int r, int g, int b)
    {
        if (Cts.IsCancellationRequested) return;

        Aura.ApplyAura(Color.FromArgb(r, g, b));
        Console.WriteLine($"{r} {g} {b}");
        Cts.Token.WaitHandle.WaitOne(_sleepBetweenIterations);
    }
}