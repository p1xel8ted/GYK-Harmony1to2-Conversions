using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(BuildModeLogics), "CanBuild", null)]
    internal class BuildModeLogics1
    {
        [HarmonyPrefix]
        private static void CanBuild(BuildModeLogics __instance)
        {
            if (!Options.DoCraftFromAnywhere)
                return;
            Traverse.Create(__instance).Field("_multi_inventory").SetValue(MainGame.me.player.GetMultiInventoryForInteraction());
        }
    }
}