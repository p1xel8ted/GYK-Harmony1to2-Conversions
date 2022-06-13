using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (GameSave), "InitPlayersInventory", null)]
  internal class GameSave3
  {
    [HarmonyPostfix]
    private static void InitPlayersInventory(GameSave __instance)
    {
      Item savedPlayerInventory = __instance.GetSavedPlayerInventory();
      savedPlayerInventory.SetInventorySize(savedPlayerInventory.inventory_size + Entry.Config.playerAdditional);
    }
  }
}
