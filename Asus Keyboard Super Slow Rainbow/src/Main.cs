// See https://aka.ms/new-console-template for more information

using System.Drawing;
using Asus_Keyboard_Super_Slow_Rainbow;
using static System.Console;

#pragma warning disable CS8321


var cts = new CancellationTokenSource();
var ct = cts.Token;
new RainbowColors(TimeSpan.FromSeconds(1000), ct: ct).DoTheRainbow();


void RandomAttempts()
{
    while (true)
    {
        // var red = Color.Red;
        // var yellow = Color.Yellow;
        // var green = Color.Green;
        // var cyan = Color.Cyan;
        // var blue = Color.Blue;
        // var purple = Color.Purple;

        // Console.WriteLine(red.GetHue() + " " + red.GetSaturation() + " " + red.GetBrightness());
        // Console.WriteLine(yellow.GetHue() + " " + yellow.GetSaturation() + " " + yellow.GetBrightness());
        // Console.WriteLine(green.GetHue() + " " + green.GetSaturation() + " " + green.GetBrightness());
        // Console.WriteLine(cyan.GetHue() + " " + cyan.GetSaturation() + " " + cyan.GetBrightness());
        // Console.WriteLine(blue.GetHue() + " " + blue.GetSaturation() + " " + blue.GetBrightness());
        // Console.WriteLine(purple.GetHue() + " " + purple.GetSaturation() + " " + purple.GetBrightness());


        // //### Red ➡️ Orange ➡️ Yellow
        // CycleColorsUp(red, yellow);
        // // ### Yellow ➡️ Green
        // CycleColorsDown(yellow, green);
        // // ### Green ➡️ Cyan
        // CycleColorsUp(green, cyan);
        // // ### Cyan ➡️ Blue
        // CycleColorsDown(cyan, blue);
        // // ### Blue ➡️ Purple
        // CycleColorsUp(blue, purple);
        // // ### Purple ➡️ Red
        // CycleColorsDown(purple, red);
    }
    // ReSharper disable once FunctionNeverReturns
}

void CycleColorsUp(Color from, Color to)
{
    for (int r = from.R; r <= to.R; r += 10)
    for (int g = from.G; g <= to.G; g += 10)
    for (int b = from.B; b <= to.B; b += 10)
    {
        Aura.ApplyAura(Color.FromArgb(r, g, b));
        WriteLine($"{r} {g} {b}");
        Thread.Sleep(TimeSpan.FromSeconds(0.0001));
    }
}

void CycleColorsDown(Color from, Color to)
{
    for (int r = from.R; r <= to.R; r -= 10)
    for (int g = from.G; g <= to.G; g -= 10)
    for (int b = from.B; b <= to.B; b -= 10)
    {
        Aura.ApplyAura(Color.FromArgb(r, g, b));
        WriteLine($"{r} {g} {b}");
        Thread.Sleep(TimeSpan.FromSeconds(0.0001));
    }
}


static class Util
{
    public static void MeasureDuration(Action f)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        f();

        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        WriteLine(elapsedMs + "ms");
    }
}