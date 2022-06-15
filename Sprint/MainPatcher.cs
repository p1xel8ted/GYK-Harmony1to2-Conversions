using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace Sprint
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("com.glibfire.graveyardkeeper.sprint.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Sprint (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }
    }
}