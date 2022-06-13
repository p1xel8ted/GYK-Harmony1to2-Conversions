using HarmonyLib;
using UnityEngine;

namespace MoreStorage
{
  [HarmonyPatch(typeof (WorldGameObject), "GetMultiInventoryForInteraction", null)]
  internal class WorldGameObject3
  {
    [HarmonyPostfix]
    private static void GetMultiInventoryForInteraction(
      ref WorldGameObject __instance,
      ref MultiInventory __result)
    {
      if (!Entry.Config.doCraftFromAnywhere)
        return;
      WorldGameObject nearest = __instance.components.interaction.nearest;
      if ((Object) nearest == (Object) null)
      {
        __instance.obj_def.additional_worldzone_inventories = Entry.ZONES;
        __result = __instance.GetMultiInventory(force_world_zone: ((string) null), include_toolbelt: true, include_bags: true);
      }
      else
        __result = nearest.GetMultiInventory(force_world_zone: ((string) null), include_toolbelt: true, include_bags: true);
    }
  }
}
