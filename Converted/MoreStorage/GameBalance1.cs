using HarmonyLib;

namespace MoreStorage
{
  [HarmonyPatch(typeof (GameBalance), "LoadGameBalance", null)]
  internal class GameBalance1
  {
    [HarmonyPostfix]
    private static void LoadGameBalance()
    {
      foreach (ItemDefinition itemDefinition in GameBalance.me.items_data)
      {
        if (itemDefinition.stack_count > 1 && itemDefinition.stack_count < 99999)
        {
          int num = (int) ((double) itemDefinition.stack_count * (double) Entry.Config.stackSizeMult);
          if (num <= 0)
            num = 1;
          itemDefinition.stack_count = num;
        }
      }
    }
  }
}
