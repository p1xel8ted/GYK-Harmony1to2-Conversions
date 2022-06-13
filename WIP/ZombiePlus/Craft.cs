using System;
using Harmony;
using HarmonyLib;
using UnityEngine;
using Random = System.Random;

namespace ZombiePlus
{
  public static class Craft
  {
    public static int RandomBetween(int a, int b) => new Random().Next(a, b);

    public static void ModifyCraftObject(
      ref CraftComponent __instance,
      string objIdContains,
      string craftIdContains,
      Action<CraftDefinition> act)
    {
      if (!__instance.wgo.obj_id.Contains(objIdContains))
        return;
      GameBalance.me.GetCraftsForObject(__instance.wgo.obj_id).ForEach(craft =>
      {
          if (!craft.id.Contains(craftIdContains))
              return;
          act(craft);
      });
    }

    public static void ReduceSeedsNeedToZombieCraft(ref CraftComponent __instance) => ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", craft => craft.needs.ForEach(n => n.value = Entry.Config.Craft_ZombieFarm_Garden_Needs_Value));

    public static void AddCropWasteToZombieCraft(ref CraftComponent __instance) => ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", craft =>
    {
        if (craft.output.Exists(o => o.id == "crop_waste"))
            return;
        var obj = new Item("crop_waste")
        {
            min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
            max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
            self_chance = {
                default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance
            }
        };
        craft.output.Add(obj);
    });

    public static void AddItemToCraft(
      ref CraftComponent instance,
      string objIdContain,
      string craftIdContain,
      Item item)
    {
      Predicate<Item> v9__1;
      ModifyCraftObject(ref instance, objIdContain, craftIdContain, craft =>
      {
          if (craft.output.Exists(v9__1 = o => o.id == item.id))
              return;
          craft.output.Add(item);
      });
    }

    public static void SetNeedsToCraft(
      ref CraftComponent instance,
      string objIdContain,
      string craftIdContain,
      int value)
    {
      Action<Item> v9__1;
      ModifyCraftObject(ref instance, objIdContain, craftIdContain, craft => craft.needs.ForEach(v9__1 = n => n.value = value));
    }

    public static void CraftModifyNeeds(string craftPlaceContains, int value, bool allNeeds = false)
    {
      try
      {
        Action<Item> v9__1;
        GameBalance.me.craft_data.ForEach(craft =>
        {
            if (craft == null || craft.needs == null || craft.needs.Count == 0 || !allNeeds || !craft.id.Contains(craftPlaceContains))
                return;
            craft.needs.ForEach(v9__1 = needs =>
            {
                if (!needs.id.Contains("seeds"))
                    return;
                needs.value = value;
            });
        });
      }
      catch (Exception ex)
      {
        Debug.Log(string.Format("[ZombieEnhanced] Couldn't modify output: {0} vl:{1} an:{2} e={3}", craftPlaceContains, value, allNeeds, ex));
      }
    }

    public static void CraftAddOutput(string craftPlaceContains, Item item)
    {
      try
      {
        Predicate<Item> v9__1;
        GameBalance.me.craft_data.ForEach(craft =>
        {
            if (craft?.output == null || craft.output.Count == 0 || !craft.id.Contains(craftPlaceContains) || craft.output.Exists(v9__1 = o => o.id == item.id))
                return;
            craft.output.Add(item);
        });
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[ZombiePlus] Couldn't modify output: {0} {1}", craftPlaceContains, ex));
      }
    }

    [HarmonyPatch(typeof (CraftComponent))]
    [HarmonyPatch("FillCraftsList")]
    internal class FillCraftsListPatcher
    {
      [HarmonyPrefix]
      public static void FillCraftsList_Prefix(CraftComponent __instance)
      {
        if (Entry.Config.Craft_ZombieFarm_SeedsNeed_Enabled)
        {
          SetNeedsToCraft(ref __instance, "zombie_garden_desk", "grow_desk_planting", Entry.Config.Craft_ZombieFarm_Garden_Needs_Value);
          SetNeedsToCraft(ref __instance, "zombie_vineyard_desk", "grow_vineyard_planting", Entry.Config.Craft_ZombieFarm_Vineyard_Needs_Value);
        }
        if (!Entry.Config.Craft_ZombieFarm_ProduceWaste_Enabled)
          return;
        var obj = new Item("crop_waste")
        {
          min_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Min.ToString()),
          max_value = SmartExpression.ParseExpression(Entry.Config.Craft_ZombieFarm_ProduceWaste_Max.ToString()),
          self_chance = {
            default_value = Entry.Config.Craft_ZombieFarm_ProduceWaste_Chance
          }
        };
        AddItemToCraft(ref __instance, "zombie_vineyard_desk", "vineyard_planting", obj);
        AddItemToCraft(ref __instance, "zombie_garden_desk", "grow_desk_planting", obj);
      }
    }
  }
}
