using HarmonyLib;
using System.Linq;

namespace SharedInv
{
    internal class GameBalancePatcher
    {
        private static void LogZones()
        {
            var worldZoneDefinition1 = GameBalance.me.world_zones_data.Last();
            Helper.Log("Dumping game world zones");
            Helper.Log("-------------------------------------");
            foreach (var worldZoneDefinition2 in GameBalance.me.world_zones_data)
            {
                if (worldZoneDefinition2.Equals(worldZoneDefinition1))
                {
                    Helper.Log($"\"{(object)worldZoneDefinition2.id}\"");
                    Helper.Log("-------------------------------------");
                    break;
                }

                Helper.Log($"\"{(object)worldZoneDefinition2.id}\",");
            }
        }

        [HarmonyPatch(typeof(GameBalance))]
        [HarmonyPatch("LoadGameBalance")]
        internal class GameBalanceUpdatePatch
        {
            [HarmonyPostfix]
            private static void LoadGameBalance()
            {
                var config = Helper.GetConfig();
                if (config == null || !config.LogZones)
                    return;
                LogZones();
            }
        }
    }
}