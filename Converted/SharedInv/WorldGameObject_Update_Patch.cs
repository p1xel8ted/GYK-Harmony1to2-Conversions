using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace SharedInv
{
    [HarmonyPatch(typeof(WorldGameObject))]
    [HarmonyPatch("GetMultiInventory")]
    internal class WorldGameObject_Update_Patch
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
                Helper.Config config = Helper.GetConfig();
                if (config == null || !config.includePlayer && __instance.is_player || !config.includeCrafting &&
                    __instance.obj_def.interaction_type == ObjectDefinition.InteractionType.Craft)
                    return;
                List<Inventory> inventoryList =
                    WorldGameObject_Update_Patch.SharedInventories(__instance.GetMyWorldZone());
                foreach (Inventory inventory in inventoryList
                             .Where<Inventory>((Func<Inventory, bool>) (inv => inv != null))
                             .Where<Inventory>((Func<Inventory, bool>) (inv => inv.data.inventory.Count == 0)))
                    inventoryList.Remove(inventory);
                __result = new MultiInventory();
                __result.SetInventories(inventoryList);
            }
            catch (Exception ex)
            {
                Helper.Log(ex.ToString(), true);
            }
        }

        internal static List<Inventory> SharedInventories(
            WorldZone currentZone,
            bool include_toolbelt = false)
        {
            List<Inventory> inventoryList = new List<Inventory>()
            {
                new Inventory(MainGame.me.player.data, "Player", string.Empty)
            };
            if (include_toolbelt)
            {
                Item data = new Item()
                {
                    inventory = MainGame.me.player.data.secondary_inventory,
                    inventory_size = 7
                };
                inventoryList.Add(new Inventory(data, "Toolbelt", string.Empty));
            }

            foreach (Inventory inventory in WorldGameObject_Update_Patch.GetAdditionalZones()
                         .Select<string,
                             WorldZone>((Func<string, WorldZone>) (additionalZone =>
                             WorldZone.GetZoneByID(additionalZone, false)))
                         .Where<
                             WorldZone>((Func<WorldZone, bool>) (zoneById =>
                             (UnityEngine.Object) zoneById != (UnityEngine.Object) null))
                         .Select<WorldZone, List<Inventory>>((Func<WorldZone, List<Inventory>>) (zoneById =>
                             zoneById.GetMultiInventory(player_mi: MultiInventory.PlayerMultiInventory.ExcludePlayer,
                                 sortWGOS: true)))
                         .Where<List<Inventory>>(
                             (Func<List<Inventory>, bool>) (multiInventory => multiInventory != null))
                         .SelectMany<List<Inventory>, Inventory, Inventory>(
                             (Func<List<Inventory>, IEnumerable<Inventory>>) (multiInventory =>
                                 (IEnumerable<Inventory>) multiInventory),
                             (Func<List<Inventory>, Inventory, Inventory>) ((multiInventory, inventory) => inventory)))
            {
                if (inventory == null || inventory.data.inventory.Count != 0)
                    inventoryList.Add(inventory);
            }

            return inventoryList;
        }

        internal static List<string> GetAdditionalZones()
        {
            Helper.Config config = Helper.GetConfig();
            List<string> additionalZones = new List<string>();
            if (config.automatic)
                additionalZones.AddRange(GameBalance.me.world_zones_data
                    .Where<WorldZoneDefinition>((Func<WorldZoneDefinition, bool>) (worldZoneDefinition =>
                        !config.excluded.Contains(worldZoneDefinition.id)))
                    .Select<WorldZoneDefinition, string>(
                        (Func<WorldZoneDefinition, string>) (worldZoneDefinition => worldZoneDefinition.id)));
            else
                additionalZones = config.included;
            return additionalZones;
        }
    }
}