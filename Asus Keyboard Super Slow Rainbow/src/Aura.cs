using System.Drawing;
using HidLibrary;
using static System.Console;

namespace Asus_Keyboard_Super_Slow_Rainbow;

// Stolen from https://github.com/JamesCJ60/Armoury-Control/blob/master/acControl/Scripts/Aura.cs
//
// This is an optimised/simplified version of Aura.cs from https://github.com/seerge/g-helper
// I do not take credit for the full functionality of the code.
//

public static class Aura
{
    // ReSharper disable once InconsistentNaming
    private static readonly byte[] MESSAGE_SET = { 0x5d, 0xb5, 0, 0, 0 };

    // ReSharper disable once InconsistentNaming
    private static readonly byte[] MESSAGE_APPLY = { 0x5d, 0xb4, 0, 0, 0 };

    private const int ModeStatic = 0;

    private const int SpeedSlow = 0;

    private static readonly List<HidDevice> AuraDevices = new();

    static Aura()
    {
        _refreshAuraDevices();
    }

    private static void _refreshAuraDevices()
    {
        int[] deviceIds = { 0x1854, 0x1869, 0x1866, 0x19b6, 0x1822, 0x1837, 0x1854, 0x184a, 0x183d, 0x8502, 0x1807, 0x17e0 };
        foreach (var device in HidDevices.Enumerate(0x0b05, deviceIds))
        {
            device.OpenDevice();
            if (device.Write(MESSAGE_SET))
            {
                AuraDevices.Add(device);
            }

            device.CloseDevice();
        }

        WriteLine($"AuraDevices:");
        foreach (var d in AuraDevices)
        {
            Write($"    {d}, ");
        }
    }

    public static void ApplyAura(Color color)
    {
        // MeasureDuration(() => _applyAura(color));
        _applyAura(color);
    }

    private static void _applyAura(Color color)
    {
        foreach (var device in AuraDevices)
        {
            if (!device.IsOpen)
            {
                device.OpenDevice();
            }

            byte[] msg =
            {
                0x5d, 0xb3, 0x00, ModeStatic,
                color.R, color.G, color.B,
                SpeedSlow, 0, 0,
                color.R, color.G, color.B
            };
            device.Write(msg);
            device.Write(MESSAGE_SET);
            device.Write(MESSAGE_APPLY);
        }
    }
}