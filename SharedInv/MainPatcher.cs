using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace SharedInv
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                Helper.RemoveLogs();

                var harmony = new Harmony("com.roberts.graveyardkeeper.SharedInv.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SharedInv (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }
    }
}