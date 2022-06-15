using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(WorldGameObject), "GetMultiInventoryForInteraction", null)]
    internal class WorldGameObject3
    {
        [HarmonyPostfix]
        private static void GetMultiInventoryForInteraction(
          ref WorldGameObject __instance,
          ref MultiInventory __result)
        {
            if (!Options.DoCraftFromAnywhere)
                return;
            var nearest = __instance.components.interaction.nearest;
            if (nearest == null)
            {
                __instance.obj_def.additional_worldzone_inventories = MainPatcher.Zones;
                __result = __instance.GetMultiInventory(force_world_zone: null, include_toolbelt: true, include_bags: true);
            }
            else
                __result = nearest.GetMultiInventory(force_world_zone: null, include_toolbelt: true, include_bags: true);
        }
    }
}