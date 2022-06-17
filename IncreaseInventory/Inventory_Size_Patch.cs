using HarmonyLib;

namespace IncreaseInventory
{
    [HarmonyPatch(typeof(Inventory))]
    [HarmonyPatch("size", MethodType.Getter)]
    internal class InventorySizePatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref int __result, ref Inventory __instance)
        {
            if (!__instance.name.Contains("offer"))
            {
                __result = MainPatcher.NewSize;
            }
        }
    }
}