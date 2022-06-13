using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace AlwaysShowExperienceBar
{
  public class AlwaysShowExperienceBar
  {
    [HarmonyPatch(typeof (PlayerComponent), "SpawnPlayer", null)]
    public class PlayerComponent_SpawnPlayer
    {
      [HarmonyPostfix]
      public static void Postfix()
      {
        if (MainPatcher.fieldStayShownTime == (FieldInfo) null)
          MainPatcher.fieldStayShownTime = typeof (AnimatedGUIPanel).GetField("stay_shown_time", AccessTools.all);
        if (!(MainPatcher.fieldStayShownTime != (FieldInfo) null))
          return;
        GUIElements.me.hud.tech_points_bar.Init();
        MainPatcher.fieldStayShownTime.SetValue((object) GUIElements.me.hud.tech_points_bar, (object) float.PositiveInfinity);
        GUIElements.me.hud.tech_points_bar.Show();
      }
    }

    [HarmonyPatch(typeof (WorldGameObject), "UseItemFromInventory", new System.Type[] {typeof (Item), typeof (Vector3?), typeof (Item)})]
    public class WorldGameObject_UseItemFromInventory
    {
      [HarmonyPostfix]
      public static void Postfix() => GUIElements.me.hud.tech_points_bar.Redraw();
    }

    [HarmonyPatch(typeof (BaseCharacterComponent), "UseItemFromToolbar", new System.Type[] {typeof (int)})]
    public class BaseCharacterComponent_UseItemFromToolbar
    {
      [HarmonyPostfix]
      public static void Postfix() => GUIElements.me.hud.tech_points_bar.Redraw();
    }

    [HarmonyPatch(typeof (Item), "UseItem", new System.Type[] {typeof (WorldGameObject), typeof (Vector3?)})]
    public class Item_UseItem
    {
      [HarmonyPostfix]
      public static void Postfix() => GUIElements.me.hud.tech_points_bar.Redraw();
    }

    [HarmonyPatch(typeof (InventoryGUI), "UseItem", new System.Type[] {})]
    public class InventoryGUI_UseItem
    {
      [HarmonyPostfix]
      public static void Postfix() => GUIElements.me.hud.tech_points_bar.Redraw();
    }

    [HarmonyPatch(typeof (TechConnector), "SetState", new System.Type[] {typeof (TechDefinition.TechState)})]
    public class TechConnector_SetState
    {
      [HarmonyPostfix]
      public static void Postfix(TechDefinition.TechState state)
      {
        if (state != TechDefinition.TechState.Purchased)
          return;
        GUIElements.me.hud.tech_points_bar.Redraw();
      }
    }
  }
}
