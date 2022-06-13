using System.Collections.Generic;
using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (WorldZone), "GetMultiInventory", null)]
  internal class WorldZone1
  {
    [HarmonyPostfix]
    private static void GetMultiInventory(ref List<Inventory> __result)
    {
      if (__result != null)
        return;
      List<Inventory> inventoryList = new List<Inventory>();
      Item obj = new Item()
      {
        inventory = new List<Item>(),
        inventory_size = 0
      };
      __result = inventoryList;
    }
  }
}
