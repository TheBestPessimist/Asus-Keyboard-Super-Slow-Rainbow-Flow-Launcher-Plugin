// See https://aka.ms/new-console-template for more information

using System.Drawing;
using Asus_Keyboard_Super_Slow_Rainbow;

#pragma warning disable CS8321


var cts = new CancellationTokenSource();
var ct = cts.Token;
new RainbowColors(TimeSpan.FromSeconds(1000), ct: ct).DoTheRainbow();

