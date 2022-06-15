using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(GameSave), "FromBinary", null)]
    internal class GameSave1
    {
        [HarmonyPostfix]
        private static void FromBinary(GameSave __result)
        {
            var size = 20 + Options.PlayerAdditional;
            var savedPlayerInventory = __result.GetSavedPlayerInventory();
            if (savedPlayerInventory.inventory_size == size)
                return;
            savedPlayerInventory.SetInventorySize(size);
        }
    }
}