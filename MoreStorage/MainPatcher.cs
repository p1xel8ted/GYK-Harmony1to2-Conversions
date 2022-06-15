using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MoreStorage
{
    public static class MainPatcher
    {
        public static readonly List<string> Zones = new List<string>
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
        private const string ConfigPath = "./QMods/MoreStorage/config.txt";
        private const string LogPath = "./QMods/MoreStorage/log.txt";

        public static bool IsValidStorage(ObjectDefinition def)
        {
            if (def.interaction_type != ObjectDefinition.InteractionType.Chest)
                return false;
            return def.inventory_size == 25 || def.inventory_size == 20;
        }

        public static void Log(string v)
        {
            using (var streamWriter = File.AppendText(LogPath))
                streamWriter.WriteLine(v);
        }

        public static void Patch()
        {
            LoadSettings();
            try
            {
                var harmony = new Harmony("com.captaindapper.graveyardkeeper.morestorage.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[MoreStorage (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }

        private static T Deserialize<T>(string fileName)
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);
            foreach (var readLine in File.ReadLines(fileName))
            {
                var strArray = readLine.Split('=');
                if (strArray.Length != 2) continue;
                var property = type.GetProperty(strArray[0].Trim());
                if (property != null)
                    property.SetValue(instance, Convert.ChangeType(strArray[1], property.PropertyType));
            }
            return (T)instance;
        }

        private static void LoadSettings()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    Config = new Options();
                    WriteSettings(Config);
                }
                else
                {
                    Config = Deserialize<Options>(ConfigPath);
                    WriteSettings(Config);
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        private static void WriteSettings(Options cfg)
        {
            using (var text = File.CreateText(ConfigPath))
            {
                foreach (var property in cfg.GetType().GetProperties())
                    text.WriteLine($"{property.Name}={property.GetValue(cfg)}");
            }
        }
    }
}