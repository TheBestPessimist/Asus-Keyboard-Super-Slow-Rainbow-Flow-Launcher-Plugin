Stop-Process -Name "Flow.Launcher"

dotnet publish "Flow.Launcher.Plugin.Asus Keyboard Super Slow Rainbow Flow Launcher Plugin" -c Release -r win-x64 --no-self-contained
Compress-Archive -LiteralPath "Flow.Launcher.Plugin.Asus Keyboard Super Slow Rainbow Flow Launcher Plugin/bin/Release/win-x64/publish" -DestinationPath "Flow.Launcher.Plugin.Asus Keyboard Super Slow Rainbow Flow Launcher Plugin/bin/Asus Keyboard Super Slow Rainbow Flow Launcher Plugin.zip" -Force
robocopy /E /Z /R:5 /W:5 /TBD /unicode /V /XJ /ETA /MT:32       'D:\all\work\Asus Keyboard Super Slow Rainbow Flow Launcher Plugin\Flow.Launcher.Plugin.Asus Keyboard Super Slow Rainbow Flow Launcher Plugin\bin\Release\win-x64\publish' 'C:\Users\TheBestPessimist\AppData\Roaming\FlowLauncher\Plugins\Asus-1.0.0'

& "D:/all/work/Flow.Launcher/Output/Debug/Flow.Launcher.exe"
