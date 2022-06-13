using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace MoreStorage
{
  public class Entry
  {
    public const int PLAYER_DEFAULT = 20;
    public const string CONFIG_PATH = ".\\QMods\\MoreStorage\\config.txt";
    public const string LOG_PATH = ".\\QMods\\MoreStorage\\log.txt";
    public static readonly List<string> ZONES = new List<string>()
    {
      "alarich_tent_inside",
      "alchemy",
      "beatch",
      "beegarden",
      "burned_house",
      "camp",
      "cellar",
      "cellar_storage",
      "church",
      "cliff",
      "cremation",
      "east_border",
      "euric_room",
      "flat_under_waterflow",
      "flat_under_waterflow_2",
      "flat_under_waterflow_3",
      "forest_under_village",
      "garden",
      "graveyard",
      "hill",
      "home",
      "marble_deposit",
      "mf_wood",
      "morgue",
      "morgue_outside",
      "nountain_fort",
      "players_tavern",
      "player_tavern_cellar",
      "refugees_camp",
      "sacrifice",
      "sealight",
      "souls",
      "stone_workyard",
      "storage",
      "swamp",
      "tavern",
      "tree_garden",
      "vilage",
      "vineyard",
      "wheat_land",
      "witch_hut",
      "zombie_sawmill"
    };
    public static Options Config;

    public static void Patch()
    {
      Entry.LoadSettings();
      try
      { 
          var harmony = new Harmony("com.captaindapper.graveyardkeeper.morestorage.mod");
        var assembly = Assembly.GetExecutingAssembly();
        harmony.PatchAll(assembly);

      }
      catch (Exception ex)
      {
        Entry.Log(ex.ToString());
      }
    }

    public static void Log(string v)
    {
      using (StreamWriter streamWriter = File.AppendText(".\\QMods\\MoreStorage\\log.txt"))
        streamWriter.WriteLine(v);
    }

    public static bool IsValidStorage(ObjectDefinition def)
    {
      if (def.interaction_type != ObjectDefinition.InteractionType.Chest)
        return false;
      return def.inventory_size == 25 || def.inventory_size == 20;
    }

    private static void LoadSettings()
    {
      try
      {
        if (!File.Exists(".\\QMods\\MoreStorage\\config.txt"))
        {
          Entry.Config = new Options();
          Entry.WriteSettings(Entry.Config);
        }
        else
        {
          Entry.Config = Entry.Deserialize<Options>(".\\QMods\\MoreStorage\\config.txt");
          Entry.WriteSettings(Entry.Config);
        }
      }
      catch (Exception ex)
      {
        Entry.Log(ex.ToString());
      }
    }

    private static void WriteSettings(Options cfg)
    {
      using (StreamWriter text = File.CreateText(".\\QMods\\MoreStorage\\config.txt"))
      {
        foreach (PropertyInfo property in cfg.GetType().GetProperties())
          text.WriteLine(string.Format("{0}={1}", (object) property.Name, property.GetValue((object) cfg)));
      }
    }

    private static T Deserialize<T>(string fileName)
    {
      System.Type type = typeof (T);
      object instance = Activator.CreateInstance(type);
      foreach (string readLine in File.ReadLines(fileName))
      {
        string[] strArray = readLine.Split('=');
        if (strArray.Length == 2)
        {
          PropertyInfo property = type.GetProperty(strArray[0].Trim());
          if (property != (PropertyInfo) null)
            property.SetValue(instance, Convert.ChangeType((object) strArray[1], property.PropertyType));
        }
      }
      return (T) instance;
    }
  }
}
