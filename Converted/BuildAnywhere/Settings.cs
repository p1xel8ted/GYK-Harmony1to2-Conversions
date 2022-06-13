using Newtonsoft.Json;
using System;
using UnityEngine;

namespace BuildAnywhere
{
  public class Settings
  {
    [JsonIgnore]
    public KeyCode _ButtonIgnoreBuildArea;
    public string ButtonIgnoreBuildArea;
    [JsonIgnore]
    public KeyCode _ButtonIgnoreBuildOverlap;
    public string ButtonIgnoreBuildOverlap;
    public bool Hotkeys;
    [JsonIgnore]
    public KeyCode _TeleportStone;
    public string TeleportStone;
    [JsonIgnore]
    public KeyCode _TeleportMouse;
    public string TeleportMouse;
    public bool FishingAlwaysSuccess;
    public bool FishAlwaysBiting;
    public bool FishPullAutomatically;
    public bool FishNoDurabiliyLoss;
    public bool AllObjectsNoDurability;
    public bool AutopsyNoConfirm;
    public bool AutoDropIntoStorage;
    public bool AutoDropIntoStoragePlayerStacks;
    public bool AutoDropIntoStoragePlayerAll;
    public bool ZombieDropTechOrbs;
    public bool PGB_global;
    public bool PGB_LinkStorageWellPump;
    public bool PGB_MoreZombieRecipies;
    public bool PGB__AllBuildingsAtAllBlueprintDesks;
    public bool CorpseBuff;
    public bool InfiniteBuffs;
    public bool VariousTests;
    public static Settings i = new Settings();
    public static string Path = System.IO.Path.Combine("QMods", "BuildAnywhere", "settings.json");

    public void ParseEnums()
    {
      Enum.TryParse<KeyCode>(ButtonIgnoreBuildArea, true, out _ButtonIgnoreBuildArea);
      Enum.TryParse<KeyCode>(ButtonIgnoreBuildOverlap, true, out _ButtonIgnoreBuildOverlap);
      Enum.TryParse<KeyCode>(TeleportStone, true, out _TeleportStone);
      Enum.TryParse<KeyCode>(TeleportMouse, true, out _TeleportMouse);
      Debug.Log(string.Format("[BuildAnywhere] Mapping buttons: _ButtonIgnoreBuildArea={0}, _ButtonIgnoreBuildOverlap={1}, _TeleportStone={2}, _TeleportMouse={3}", i._ButtonIgnoreBuildArea, _ButtonIgnoreBuildOverlap, _TeleportStone, _TeleportMouse));
    }

    public Settings()
    {
      ButtonIgnoreBuildArea = ((KeyCode) 99).ToString();
      ButtonIgnoreBuildOverlap = ((KeyCode) 118).ToString();
      Hotkeys = true;
      TeleportStone = ((KeyCode) 104).ToString();
      TeleportMouse = ((KeyCode) 103).ToString();
      FishingAlwaysSuccess = true;
      FishAlwaysBiting = true;
      FishPullAutomatically = true;
      FishNoDurabiliyLoss = true;
      AllObjectsNoDurability = true;
      AutopsyNoConfirm = true;
      AutoDropIntoStorage = true;
      ZombieDropTechOrbs = true;
      PGB_global = true;
      PGB_LinkStorageWellPump = true;
      PGB_MoreZombieRecipies = true;
      CorpseBuff = true;
      InfiniteBuffs = true;
    }
  }
}
