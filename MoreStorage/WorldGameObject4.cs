using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(WorldGameObject), "GetMultiInventory", null)]
    internal class WorldGameObject4
    {
        [HarmonyPrefix]
        private static void GetMultiInventory(
          ref WorldGameObject __instance,
          ref MultiInventory __result)
        {
            if (!Options.DoCraftFromAnywhere)
                return;
            __instance.obj_def.additional_worldzone_inventories = MainPatcher.Zones;
        }
    }
}