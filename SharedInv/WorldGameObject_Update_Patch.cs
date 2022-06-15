using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedInv
{
    [HarmonyPatch(typeof(WorldGameObject))]
    [HarmonyPatch("GetMultiInventory")]
    internal class WorldGameObjectUpdatePatch
    {
        [HarmonyPostfix]
        public static void Postfix(
            WorldGameObject __instance,
            ref MultiInventory __result,
            List<WorldGameObject> exceptions = null,
            string force_world_zone = "",
            MultiInventory.PlayerMultiInventory player_mi = MultiInventory.PlayerMultiInventory.DontChange,
            bool include_toolbelt = false,
            bool sortWGOS = false,
            bool include_bags = false)
        {
            try
            {
                if (__instance.obj_def.interaction_type != ObjectDefinition.InteractionType.Craft &&
                    !__instance.is_player)
                    return;
                var config = Helper.GetConfig();
                if (config == null || !config.IncludePlayer && __instance.is_player || !config.IncludeCrafting &&
                    __instance.obj_def.interaction_type == ObjectDefinition.InteractionType.Craft)
                    return;
                var inventoryList =
                    SharedInventories(__instance.GetMyWorldZone());
                foreach (var inventory in inventoryList
                             .Where(inv => inv != null)
                             .Where(inv => inv.data.inventory.Count == 0))
                    inventoryList.Remove(inventory);
                __result = new MultiInventory();
                __result.SetInventories(inventoryList);
            }
            catch (Exception ex)
            {
                Helper.Log(ex.ToString(), true);
            }
        }

        private static IEnumerable<string> GetAdditionalZones()
        {
            var config = Helper.GetConfig();
            var additionalZones = new List<string>();
            if (config.Automatic)
                additionalZones.AddRange(GameBalance.me.world_zones_data
                    .Where(worldZoneDefinition =>
                        !config.Excluded.Contains(worldZoneDefinition.id))
                    .Select(
                        worldZoneDefinition => worldZoneDefinition.id));
            else
                additionalZones = config.Included;
            return additionalZones;
        }

        private static List<Inventory> SharedInventories(
                    WorldZone currentZone,
            bool include_toolbelt = false)
        {
            var inventoryList = new List<Inventory>
            {
                new(MainGame.me.player.data, "Player", string.Empty)
            };
            if (include_toolbelt)
            {
                var data = new Item
                {
                    inventory = MainGame.me.player.data.secondary_inventory,
                    inventory_size = 7
                };
                inventoryList.Add(new Inventory(data, "Toolbelt", string.Empty));
            }

            inventoryList.AddRange(GetAdditionalZones()
                .Select(additionalZone => WorldZone.GetZoneByID(additionalZone, false))
                .Where(zoneById => zoneById != null)
                .Select(zoneById => zoneById.GetMultiInventory(player_mi: MultiInventory.PlayerMultiInventory.ExcludePlayer, sortWGOS: true))
                .Where(multiInventory => multiInventory != null)
                .SelectMany(multiInventory => multiInventory, (_, inventory) => inventory)
                .Where(inventory => inventory == null || inventory.data.inventory.Count != 0));

            return inventoryList;
        }
    }
}