using System;
using System.Collections.Generic;
using Flow.Launcher.Plugin;

namespace Flow.Launcher.Plugin.Asus_Keyboard_Super_Slow_Rainbow_Flow_Launcher_Plugin
{
    public class Asus_Keyboard_Super_Slow_Rainbow_Flow_Launcher_Plugin : IPlugin
    {
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        public List<Result> Query(Query query)
        {
            return new List<Result>();
        }
    }
}