using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (BuildModeLogics), "DoPlace", null)]
  internal class BuildModeLogics2
  {
    [HarmonyPrefix]
    private static void DoPlace(BuildModeLogics __instance)
    {
      if (!Entry.Config.doCraftFromAnywhere)
        return;
      Traverse.Create((object) __instance).Field("_multi_inventory").SetValue((object) MainGame.me.player.GetMultiInventoryForInteraction());
    }
  }
}
