using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (WorldGameObject), "AddToInventory", new System.Type[] {typeof (Item)})]
  internal class WorldGameObject1
  {
    [HarmonyPrefix]
    private static void AddToInventory(WorldGameObject __instance)
    {
      Item data = __instance.data;
      int size1 = 20 + Entry.Config.playerAdditional;
      if (!__instance.is_player && Entry.IsValidStorage(__instance.obj_def))
      {
        int size2 = __instance.obj_def.inventory_size + Entry.Config.otherAdditional;
        if (data.inventory_size >= size2)
          return;
        Entry.Log("AddToInventory ran on '" + data.id + "'");
        data.SetInventorySize(size2);
      }
      else
      {
        if (!__instance.is_player || data.inventory_size == size1)
          return;
        Entry.Log("AddToInventory ran on player '" + data.id + "'");
        data.SetInventorySize(size1);
      }
    }
  }
}
