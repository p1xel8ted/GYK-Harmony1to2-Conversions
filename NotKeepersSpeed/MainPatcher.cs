using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace NotKeepersSpeed
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("com.glieseg.notkeepersspeed.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[NotKeepersSpeed (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }
    }
}