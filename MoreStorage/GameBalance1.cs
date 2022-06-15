using HarmonyLib;

namespace MoreStorage
{
    [HarmonyPatch(typeof(GameBalance), "LoadGameBalance", null)]
    internal class GameBalance1
    {
        [HarmonyPostfix]
        private static void LoadGameBalance()
        {
            foreach (var itemDefinition in GameBalance.me.items_data)
            {
                if (itemDefinition.stack_count <= 1 || itemDefinition.stack_count >= 99999) continue;
                var num = (int)(itemDefinition.stack_count * (double)MainPatcher.Config.StackSizeMult);
                if (num <= 0)
                    num = 1;
                itemDefinition.stack_count = num;
            }
        }
    }
}