using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Asus_Keyboard_Super_Slow_Rainbow;
using JetBrains.Annotations;

namespace Flow.Launcher.Plugin.Asus_Keyboard_Super_Slow_Rainbow_Flow_Launcher_Plugin
{
    /// <summary>
    /// Plug used to make Asus ROG Aura Keyboards show a very slow rainbow. Much slower than the default Aura speed.
    /// </summary>
    [UsedImplicitly]
    public class Main : IAsyncPlugin
    {
        private PluginInitContext _context;

        private RainbowColors _currentRainbow;
        private Task _currentTask;

        private Settings Settings;

        /// <inheritdoc />
        public Task<List<Result>> QueryAsync(Query query, CancellationToken token)
        {
            var result = new List<Result>();
            if (string.IsNullOrWhiteSpace(query.Search)) return Task.FromResult(result);

            int newMinutesAmount;
            if (int.TryParse(query.Search, out newMinutesAmount) && newMinutesAmount > 0)
            {
                result.Add(new Result
                {
                    Title = $"Desired duration: {newMinutesAmount} minutes",
                    AutoCompleteText = "",
                    Action = _ =>
                    {
                        _stopAndReload(newMinutesAmount);
                        _context.API.SaveSettingJsonStorage<Settings>();
                        return true;
                    }
                });
            }
            else
            {
                result.Add(new Result
                {
                    Title = $"Desired duration is not a valid natural number.",
                    AutoCompleteText = ""
                });
            }

            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public Task InitAsync(PluginInitContext context)
        {
            Settings = _context.API.LoadSettingJsonStorage<Settings>() ?? new Settings();

            _stopAndReload(Settings.Duration);
            return Task.CompletedTask;
        }

        private void _stopAndReload(int minutes)
        {
            if (_currentRainbow != null)
            {
                _currentRainbow.IsCancelRequested = true;
                _currentTask.Wait();
            }

            _currentRainbow = new RainbowColors(TimeSpan.FromMinutes(minutes));
            _currentTask = Task.Run(() => _currentRainbow.DoTheRainbow());
        }
    }
}