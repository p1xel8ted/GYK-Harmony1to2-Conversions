using System;
using System.Reflection;
using System.Threading;
using HarmonyLib;
using UnityEngine;

namespace BuildAnywhere
{
  public class MainPatcher
  {
    public static Thread hotkeys = new Thread(Hotkey.Update);

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
        hotkeys.IsBackground = true;
        hotkeys.Start();
      }
      catch (Exception ex)
      {
        Log(ex.ToString());
      }
    }

    public static void Log(string str) => Debug.Log("[BuildAnywhere] " + str);
  }
}
