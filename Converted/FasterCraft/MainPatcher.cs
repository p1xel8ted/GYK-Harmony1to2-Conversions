using System;
using System.Reflection;
using HarmonyLib;

namespace FasterCraft
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("com.glibfire.graveyardkeeper.fastercraft.mod");
                var assembly = Assembly.GetExecutingAssembly();
                harmony.PatchAll(assembly);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"FC: {ex.Message}");
            }
        }
    }
}