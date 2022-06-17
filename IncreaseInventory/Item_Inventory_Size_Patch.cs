using HarmonyLib;

namespace IncreaseInventory
{

    [HarmonyPatch(typeof(Item))]
    [HarmonyPatch("inventory_size", MethodType.Getter)]
    internal class ItemInventorySizePatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref int __result)
        {
            __result = MainPatcher.NewSize;
        }
    }
}