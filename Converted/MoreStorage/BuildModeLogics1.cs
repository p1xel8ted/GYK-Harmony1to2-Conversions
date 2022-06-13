using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (BuildModeLogics), "CanBuild", null)]
  internal class BuildModeLogics1
  {
    [HarmonyPrefix]
    private static void CanBuild(BuildModeLogics __instance)
    {
      if (!Entry.Config.doCraftFromAnywhere)
        return;
      Traverse.Create((object) __instance).Field("_multi_inventory").SetValue((object) MainGame.me.player.GetMultiInventoryForInteraction());
    }
  }
}
