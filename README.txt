Restaurant Robot Helper (source, not compiled or tested)

Build: install the .NET 6 SDK, open this folder, run `dotnet build`.
Needs Stardew Valley and SMAPI installed so ModBuildConfig can find the game DLLs.
Output: bin/Debug/net6.0/RobotHelper.dll. Copy RobotHelper.dll and manifest.json into
  Mods/RobotHelper/
Android: the game DLLs come from your Android install; if ModBuildConfig cannot find a PC
install, set <GamePath> in the .csproj to a folder holding the game's Stardew Valley.dll,
StardewModdingAPI.dll and MonoGame/xTile DLLs.
