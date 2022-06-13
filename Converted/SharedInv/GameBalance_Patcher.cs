using System.Linq;
using HarmonyLib;

namespace SharedInv
{
    internal class GameBalance_Patcher
    {
        internal static void LogZones()
        {
            WorldZoneDefinition worldZoneDefinition1 = GameBalance.me.world_zones_data.Last<WorldZoneDefinition>();
            Helper.Log("Dumping game world zones");
            Helper.Log("-------------------------------------");
            foreach (WorldZoneDefinition worldZoneDefinition2 in GameBalance.me.world_zones_data)
            {
                if (worldZoneDefinition2.Equals((object) worldZoneDefinition1))
                {
                    Helper.Log(string.Format("\"{0}\"", (object) worldZoneDefinition2.id));
                    Helper.Log("-------------------------------------");
                    break;
                }

                Helper.Log(string.Format("\"{0}\",", (object) worldZoneDefinition2.id));
            }
        }

        [HarmonyPatch(typeof(GameBalance))]
        [HarmonyPatch("LoadGameBalance")]
        internal class GameBalance_Update_Patch
        {
            [HarmonyPostfix]
            private static void LoadGameBalance()
            {
                Helper.Config config = Helper.GetConfig();
                if (config == null || !config.logZones)
                    return;
                GameBalance_Patcher.LogZones();
            }
        }
    }
}