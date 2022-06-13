using HarmonyLib;
using System.Reflection;

namespace NoTeleportCooldown
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.glibfire.graveyardkeeper.teleport");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
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