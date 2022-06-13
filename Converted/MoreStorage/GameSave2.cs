using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (GameSave), "FromJSON", null)]
  internal class GameSave2
  {
    [HarmonyPostfix]
    private static void FromJSON(GameSave __result)
    {
      int size = 20 + Entry.Config.playerAdditional;
      Item savedPlayerInventory = __result.GetSavedPlayerInventory();
      if (savedPlayerInventory.inventory_size == size)
        return;
      savedPlayerInventory.SetInventorySize(size);
    }
  }
}
