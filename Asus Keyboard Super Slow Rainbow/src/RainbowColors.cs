using System.Drawing;

namespace Asus_Keyboard_Super_Slow_Rainbow;

public class RainbowColors
{
    public readonly int Step;

    public readonly int Iterations;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    public readonly TimeSpan DesiredDuration;
    public readonly TimeSpan SleepBetweenIterations;

    public readonly CancellationTokenSource Cts;

    public RainbowColors(TimeSpan desiredDuration, int step = 5)
    {
        Step = step;
        Iterations = 256 * 6 / Step;
        DesiredDuration = desiredDuration;
        SleepBetweenIterations = desiredDuration / Iterations;
        Cts = new CancellationTokenSource();

        Console.WriteLine($"""
            Desired Duration: {DesiredDuration}
            Colors changed in 1 step: {Step}
            Total number of color changes (of steps): {Iterations}
            Sleep between each step: {SleepBetweenIterations}
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
            for (int green = 0; green <= 255; green += Step) SetRgb(255, green, 0);

            // ### Yellow ➡️ Green
            Console.WriteLine("Yellow ➡️ Green");
            for (int red = 255; red >= 0; red -= Step) SetRgb(red, 255, 0);

            // ### Green ➡️ Cyan
            Console.WriteLine("Green ➡️ Cyan");
            for (int blue = 0; blue <= 255; blue += Step) SetRgb(0, 255, blue);

            // ### Cyan ➡️ Blue
            Console.WriteLine("Cyan ➡️ Blue");
            for (int green = 255; green >= 0; green -= Step) SetRgb(0, green, 255);

            // ### Blue ➡️ Purple
            Console.WriteLine("Blue ➡️ Purple");
            for (int red = 0; red <= 255; red += Step) SetRgb(red, 0, 255);

            // ### Purple ➡️ Red
            Console.WriteLine("Purple ➡️ Red");
            for (int blue = 255; blue >= 0; blue -= Step) SetRgb(255, 0, blue);

            Console.WriteLine("============== Restart");
        }
    }


    private void SetRgb(int r, int g, int b)
    {
        if (Cts.IsCancellationRequested) return;

        Aura.ApplyAura(Color.FromArgb(r, g, b));
        Console.WriteLine($"{r} {g} {b}");
        Cts.Token.WaitHandle.WaitOne(SleepBetweenIterations);
    }
}