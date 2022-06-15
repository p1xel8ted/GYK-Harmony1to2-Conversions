using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(BuildModeLogics), "DoPlace", null)]
    internal class BuildModeLogics2
    {
        [HarmonyPrefix]
        private static void DoPlace(BuildModeLogics __instance)
        {
            if (!Options.DoCraftFromAnywhere)
                return;
            Traverse.Create(__instance).Field("_multi_inventory").SetValue(MainGame.me.player.GetMultiInventoryForInteraction());
        }
    }
}