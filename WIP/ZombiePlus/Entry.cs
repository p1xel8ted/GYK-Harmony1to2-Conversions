// Decompiled with JetBrains decompiler
// Type: ZombiePlus.Entry
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll
using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace ZombiePlus
{
  public static class Entry
  {
    public static IniConfig IniInstance = IniConfig.Instance;
    public static Options Config;

    public static void Patch()
    {
      LoadIniSettings();
      try
      {
          var harmony = new Harmony("p1xel8ted.GraveyardKeeper.QueueEverything");
          var assembly = Assembly.GetExecutingAssembly();
          harmony.PatchAll(assembly);
      }
      catch (Exception ex)
      {
        Log(ex.ToString());
      }
    }

    public static void Log(string v)
    {
      var path = ".\\QMods\\ZombiePlus\\log.txt";
      if (!File.Exists(path))
        path = "log.txt";
      using (var streamWriter = File.AppendText(path))
        streamWriter.WriteLine(v);
    }

    public static void LoadIniSettings()
    {
      try
      {
        if (!File.Exists(IniInstance.Path))
        {
          Config = new Options();
          IniInstance.WriteJsonConfig(Config);
        }
        else
          Config = IniInstance.ReadJsonConfig();
      }
      catch (Exception ex)
      {
        Log(ex.ToString());
      }
    }

    public static void ReloadSettings()
    {
      try
      {
        if (!File.Exists(IniInstance.Path))
        {
          Config = new Options();
          IniInstance.WriteJsonConfig(Config);
        }
        else
          Config = IniInstance.DeserializeConfig<Options>(IniInstance.Path);
      }
      catch (Exception ex)
      {
        Log(ex.ToString());
      }
    }
  }
}
