# Graveyard Keeper Harmony1 to Harmony2 Mod Conversions

# Discord

https://discord.gg/3FKuvwggpH

# Comments

These mods aren't my original work, I've just converted them to work with QMod Manager Reloaded which uses Harmony2.0. If an author updates (correctly) directly on Nexus, I will remove from this repo.
 
 QMod Manager Reloaded  - https://github.com/p1xel8ted/GYK-QModManagerReloaded
 
 Graveyard Keeper QMods - https://github.com/p1xel8ted/GYK-Mods-QMod
 
 Short of updating mod code for game changes, this is the only change required to convert Harmony1 to 2 (and reference the Harmony2 DLL).
 
 # Harmony 1 Patch Method
 
 ```c#
 using Harmony;
 
 HarmonyInstance.Create("p1xel8ted.GraveyardKeeper.TheSeedEqualizer").PatchAll(Assembly.GetExecutingAssembly());
 ```
 # Harmony 2 Patch Method
 
```c#
using HarmonyLib;

var harmony = new Harmony("p1xel8ted.GraveyardKeeper.TheSeedEqualizer");
harmony.PatchAll(Assembly.GetExecutingAssembly());
```
