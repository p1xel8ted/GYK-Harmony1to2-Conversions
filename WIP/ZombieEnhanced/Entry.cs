using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace ZombieEnhanced
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
          var harmony = new Harmony("com.saveroo.graveyardkeeper.zombieenhanced.mod");
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
      var path = ".\\QMods\\ZombieEnhanced\\log.txt";
      if (!File.Exists(path))
        path = "log.txt";
      using (var streamWriter = File.AppendText(path))
        streamWriter.WriteLine(v);
    }

    public static void LogItemData(string v, string filename = "item")
    {
      var path = ".\\QMods\\ZombieEnhanced\\" + filename + ".txt";
      if (!File.Exists(path))
        path = filename + ".txt";
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
