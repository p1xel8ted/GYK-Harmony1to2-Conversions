using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace NoTeleportCooldown
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("com.glibfire.graveyardkeeper.teleport");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[NoTeleportCooldown (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch(nameof(Item.GetGrayedCooldownPercent))]
        public static class ItemGetGrayedCooldownPercentPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref Item __instance, ref int __result)
            {
                if (__instance == null) return;
                if (__instance.id == "hearthstone")
                {
                    __result = 0;
                }
            }
        }
    }
}