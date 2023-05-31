using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Asus_Keyboard_Super_Slow_Rainbow;
using JetBrains.Annotations;

namespace Flow.Launcher.Plugin.Asus_Keyboard_Super_Slow_Rainbow
{
    /// <summary>
    /// Plug used to make Asus ROG Aura Keyboards show a very slow rainbow. Much slower than the default Aura speed.
    /// </summary>
    [UsedImplicitly]
    public class Main : IAsyncPlugin
    {
        private PluginInitContext _context = null!;

        private RainbowColors? _currentRainbow;
        private Task _currentTask = null!;

        private Settings _settings = null!;

        /// <inheritdoc />
        public Task<List<Result>> QueryAsync(Query query, CancellationToken token)
        {
            var result = new List<Result>();
            if (string.IsNullOrWhiteSpace(query.Search))
            {
                result.Add(new Result
                {
                    Title = $"Please enter a natural number.",
                    AutoCompleteText = ""
                });
            }
            else if (int.TryParse(query.Search, out var newMinutesAmount) && newMinutesAmount > 0)
            {
                result.Add(new Result
                {
                    Title = $"Desired duration: {newMinutesAmount} minutes",
                    AutoCompleteText = "",
                    AsyncAction = _ =>
                    {
                        _stopAndReload(newMinutesAmount);
                        return new ValueTask<bool>(true);
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

            AppendCurrentConfiguration(result);
            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public Task InitAsync(PluginInitContext context)
        {
            _context = context;
            _settings = _context.API.LoadSettingJsonStorage<Settings>();
            _stopAndReload(_settings.Duration);
            return Task.CompletedTask;
        }

        private void _stopAndReload(int minutes)
        {
            if (_currentRainbow != null)
            {
                _currentRainbow.Cts.Cancel();
                _currentTask.Wait();
            }

            _currentRainbow = new RainbowColors(TimeSpan.FromMinutes(minutes), 10);
            _currentTask = Task.Run(() => _currentRainbow.DoTheRainbow());
        }

        private void AppendCurrentConfiguration(List<Result> result)
        {
            if (_currentRainbow != null)
            {
                result.Add(new Result
                {
                    SubTitle = $"Current configuration:",
                    AutoCompleteText = ""
                });
                result.Add(new Result
                {
                    Title = $"Desired Duration",
                    SubTitle = $"{_currentRainbow.DesiredDuration}",
                    AutoCompleteText = ""
                });
                result.Add(new Result
                {
                    Title = $"Colors changed in 1 step",
                    SubTitle = $"{_currentRainbow.Step}",
                    AutoCompleteText = ""
                });
                result.Add(new Result
                {
                    Title = $"Total number of color changes (of steps)",
                    SubTitle = $"{_currentRainbow.Iterations}",
                    AutoCompleteText = ""
                });
                result.Add(new Result
                {
                    Title = $"Sleep between each step",
                    SubTitle = $"{_currentRainbow.SleepBetweenIterations}",
                    AutoCompleteText = ""
                });
            }
        }
    }
}