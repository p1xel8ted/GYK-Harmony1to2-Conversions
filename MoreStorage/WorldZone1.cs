using HarmonyLib;
using System.Collections.Generic;

namespace MoreStorage
{
    [HarmonyPatch(typeof(WorldZone), "GetMultiInventory", null)]
    internal class WorldZone1
    {
        [HarmonyPostfix]
        private static void GetMultiInventory(ref List<Inventory> __result)
        {
            if (__result != null)
                return;
            var inventoryList = new List<Inventory>();
            var unused = new Item
            {
                inventory = new List<Item>(),
                inventory_size = 0
            };
            __result = inventoryList;
        }
    }
}