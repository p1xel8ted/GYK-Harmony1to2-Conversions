using HarmonyLib;
using System;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace BuildAnywhere
{
    public class MainPatcher
    {
        private static readonly Thread Hotkeys = new Thread(Hotkey.Update);

        public static void Patch()
        {
            try
            {
                Log("Loading BuildAnywhere...");
                Settings.i.ParseEnums();
                if (!Settings.i.Hotkeys)
                    return;
                if (!Settings.i.VariousTests)
                    return;

                var harmony = new Harmony("com.Fumihiko.BuildAnywhere");
                var assembly = Assembly.GetExecutingAssembly();
                harmony.PatchAll(assembly);
                if (!Settings.i.Hotkeys && !Settings.i.VariousTests)
                    return;
                Hotkeys.IsBackground = true;
                Hotkeys.Start();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BuildAnywhere (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }

        private static void Log(string str) => Debug.Log("[BuildAnywhere (Harmony2)] " + str);
    }
}