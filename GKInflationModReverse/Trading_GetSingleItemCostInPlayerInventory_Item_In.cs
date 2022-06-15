﻿using HarmonyLib;

namespace GKInflationModReverse
{
    [HarmonyPatch(typeof(Trading))]
    [HarmonyPatch("GetSingleItemCostInPlayerInventory")]
    [HarmonyPatch(new[] { typeof(Item), typeof(int) })]
    internal class TradingGetSingleItemCostInPlayerInventoryItemIntPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref float __result, Item item)
        {
            if (__result == 0.0)
                return;
            var definition = item.definition;
            __result = definition.base_price;
        }
    }
}