using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(WorldGameObject), "AddToInventory", typeof(Item))]
    internal class WorldGameObject1
    {
        [HarmonyPrefix]
        private static void AddToInventory(WorldGameObject __instance)
        {
            var data = __instance.data;
            var size1 = 20 + Options.PlayerAdditional;
            if (!__instance.is_player && MainPatcher.IsValidStorage(__instance.obj_def))
            {
                var size2 = __instance.obj_def.inventory_size + Options.OtherAdditional;
                if (data.inventory_size >= size2)
                    return;
                MainPatcher.Log("AddToInventory ran on '" + data.id + "'");
                data.SetInventorySize(size2);
            }
            else
            {
                if (!__instance.is_player || data.inventory_size == size1)
                    return;
                MainPatcher.Log("AddToInventory ran on player '" + data.id + "'");
                data.SetInventorySize(size1);
            }
        }
    }
}