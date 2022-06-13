using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (WorldGameObject), "InitNewObject", null)]
  internal class WorldGameObject2
  {
    [HarmonyPostfix]
    private static void InitNewObject(WorldGameObject __instance)
    {
      if (!Entry.IsValidStorage(__instance.obj_def))
        return;
      __instance.data.SetInventorySize(__instance.obj_def.inventory_size + Entry.Config.otherAdditional);
    }
  }
}
