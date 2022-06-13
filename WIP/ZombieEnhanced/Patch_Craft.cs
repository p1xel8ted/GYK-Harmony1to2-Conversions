// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.Purist
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System;
using UnityEngine;

namespace ZombieEnhanced
{
  public static class Purist
  {
    public static int RandomBetween(int a, int b) => new Random().Next(a, b);

    public static void ModifyCraftObject(
      ref CraftComponent __instance,
      string objIdContains,
      string craftIdContains,
      Action<CraftDefinition> act)
    {
      if (!((WorldGameObjectComponentBase) __instance).wgo.obj_id.Contains(objIdContains))
        return;
      GameBalance.me.GetCraftsForObject(((WorldGameObjectComponentBase) __instance).wgo.obj_id).ForEach((Action<CraftDefinition>) (craft =>
      {
        if (!((BalanceBaseObject) craft).id.Contains(craftIdContains))
          return;
        act(craft);
      }));
    }

    public static void ReduceSeedsNeedToZombieCraft(ref CraftComponent __instance) => ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", (Action<CraftDefinition>) (craft => craft.needs.ForEach((Action<Item>) (n => n.value = Entry.Config.Craft_ZombieFarm_Garden_Needs_Value))));

    public static void AddCropWasteToZombieCraft(ref CraftComponent __instance) => ModifyCraftObject(ref __instance, "zombie_garden_desk", "grow_desk_planting", (Action<CraftDefinition>) (craft =>
    {
      if (craft.output.Exists((Predicate<Item>) (o => o.id == "crop_waste")))
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
    }));

    public static void AddItemToCraft(
      ref CraftComponent instance,
      string objIdContain,
      string craftIdContain,
      Item item)
    {
      ModifyCraftObject(ref instance, objIdContain, craftIdContain, (Action<CraftDefinition>) (craft =>
      {
        if (craft.output.Exists((Predicate<Item>) (o => o.id == item.id)))
          return;
        craft.output.Add(item);
      }));
    }

    public static void SetNeedsToCraft(
      ref CraftComponent instance,
      string objIdContain,
      string craftIdContain,
      int value)
    {
      ModifyCraftObject(ref instance, objIdContain, craftIdContain, (Action<CraftDefinition>) (craft => craft.needs.ForEach((Action<Item>) (n => n.value = value))));
    }

    public static void CraftModifyNeeds(string craftPlaceContains, int value, bool allNeeds = false)
    {
      try
      {
        GameBalance.me.craft_data.ForEach((Action<CraftDefinition>) (craft =>
        {
          if (craft == null || craft.needs == null || craft.needs.Count == 0 || !allNeeds || !((BalanceBaseObject) craft).id.Contains(craftPlaceContains))
            return;
          craft.needs.ForEach((Action<Item>) (needs =>
          {
            if (!needs.id.Contains("seeds"))
              return;
            needs.value = value;
          }));
        }));
      }
      catch (Exception ex)
      {
        Debug.Log((object) string.Format("[ZombieEnhanced] Couldn't modify output: {0} vl:{1} an:{2} e={3}", (object) craftPlaceContains, (object) value, (object) allNeeds, (object) ex));
      }
    }

    public static void CraftAddOutput(string craftPlaceContains, Item item)
    {
      try
      {
        GameBalance.me.craft_data.ForEach((Action<CraftDefinition>) (craft =>
        {
          if (craft == null || craft.output == null || craft.output.Count == 0 || !((BalanceBaseObject) craft).id.Contains(craftPlaceContains) || craft.output.Exists((Predicate<Item>) (o => o.id == item.id)))
            return;
          craft.output.Add(item);
        }));
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[ZombieEnhanced] Couldn't modify output: {0} {1}", (object) craftPlaceContains, (object) ex));
      }
    }

    [HarmonyPatch(typeof (CraftComponent))]
    [HarmonyPatch("FillCraftsList")]
    internal class FillCraftsList_Patcher
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

    [HarmonyPatch(typeof (CraftComponent))]
    [HarmonyPatch("DoAction")]
    public class PatchDoAction
    {
      [HarmonyPostfix]
      private static void PatchDoAction_Postfix(CraftComponent __instance)
      {
        if (!__instance.HasLinkedWorker())
          return;
        if (Entry.Config.Debug_Enabled)
          Entry.Log(string.Format("PatchDoAction {0} {1} id={2}", (object) __instance.HasLinkedWorker(), (object) ((WorldGameObjectComponentBase) __instance).wgo.progress, (object) ((WorldGameObjectComponentBase) __instance).wgo.linked_worker.unique_id));
        if ((double) ((WorldGameObjectComponentBase) __instance).wgo.progress < 2.0)
          HelperWorker.ZombieSaid(((WorldGameObjectComponentBase) __instance).wgo.linked_worker.unique_id.ToString(), HelperWorker.ZCraftStartProgressDialogue, ((WorldGameObjectComponentBase) __instance).wgo.linked_worker, __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
        if (((double) ((WorldGameObjectComponentBase) __instance).wgo.progress < 0.300000011920929 || (double) ((WorldGameObjectComponentBase) __instance).wgo.progress > 0.400000005960464) && ((double) ((WorldGameObjectComponentBase) __instance).wgo.progress < 0.600000023841858 || (double) ((WorldGameObjectComponentBase) __instance).wgo.progress > 0.800000011920929))
          return;
        HelperWorker.ZombieSaid(((WorldGameObjectComponentBase) __instance).wgo.linked_worker.unique_id.ToString(), HelperWorker.ZCraftProgressDialogue, ((WorldGameObjectComponentBase) __instance).wgo.linked_worker, __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
      }
    }

    [HarmonyPatch(typeof (CraftComponent))]
    [HarmonyPatch("ProcessFinishedCraft")]
    public class Craft_Patcher
    {
      [HarmonyPostfix]
      private static void ProcessFinishedCraft_Prefix(CraftComponent __instance)
      {
        if (!__instance.HasLinkedWorker())
          return;
        HelperWorker.ZombieSaid(((WorldGameObjectComponentBase) __instance).wgo.linked_worker.unique_id.ToString(), HelperWorker.ZFinishCraftDialogue, ((WorldGameObjectComponentBase) __instance).wgo.linked_worker, __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
      }
    }
  }
}
