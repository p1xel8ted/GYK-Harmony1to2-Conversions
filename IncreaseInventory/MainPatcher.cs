using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace IncreaseInventory
{
    public static class MainPatcher
    {
        public static int NewSize;

        public static void Patch()
        {
            try
            {
                string[] strArray;
                using (var streamReader = new StreamReader("./QMods/IncreaseInventory/config.txt"))
                    strArray = streamReader.ReadLine()?.Split('=');
                NewSize = Convert.ToInt32(strArray?[1]);

                var harmony = new Harmony("com.kaupcakes.graveyardkeeper.increaseinventory.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GKSleepMod (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }
    }
}