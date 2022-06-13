using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MaxButton
{
  public class MaxButtonCrafting
  {
    private static FieldInfo fieldAmount = typeof (CraftItemGUI).GetField("_amount", AccessTools.all);

    public static void AddMinAndMaxButtons(
      CraftItemGUI CraftItemGUI,
      string ParentButtonName,
      string MinMaxButtonName,
      bool isMaximum,
      WorldGameObject Craftery_wgo)
    {
      if (!CraftItemGUI.current_craft.CanCraftMultiple())
        return;
      Transform transform1 = CraftItemGUI.transform.Find("selection frame/amount buttons/" + ParentButtonName);
      transform1.transform.localPosition = new Vector3(transform1.transform.localPosition.x, -10f, transform1.transform.localPosition.z);
      transform1.GetComponent<UI2DSprite>().SetDimensions(26, 26);
      transform1.GetComponent<BoxCollider2D>().size = new Vector2(29.4f, 26f);
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(transform1.gameObject, transform1.parent);
      gameObject1.name = MinMaxButtonName;
      gameObject1.transform.localPosition = new Vector3(gameObject1.transform.localPosition.x, -31f, gameObject1.transform.localPosition.z);
      UIButton component1 = transform1.GetComponent<UIButton>();
      UIButton component2 = gameObject1.GetComponent<UIButton>();
      component2.normalSprite2D = component1.normalSprite2D;
      component2.hoverSprite2D = component1.hoverSprite2D;
      component2.pressedSprite2D = component1.pressedSprite2D;
      component2.onClick = new List<EventDelegate>();
      if (isMaximum)
        EventDelegate.Add(component2.onClick, (EventDelegate.Callback) (() => MaxButtonCrafting.SetMaximumAmount(ref CraftItemGUI, ref Craftery_wgo)));
      else
        EventDelegate.Add(component2.onClick, (EventDelegate.Callback) (() => MaxButtonCrafting.SetMinimumAmount(ref CraftItemGUI)));
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject1.GetComponent<UIEventTrigger>());
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject1.GetComponent<UIEventTrigger>());
      Transform transform2 = gameObject1.transform.Find("arrow spr");
      transform2.name = "arrow spr 1";
      transform2.transform.localPosition = new Vector3(transform2.transform.localPosition.x + 4f, transform2.transform.localPosition.y, transform2.transform.localPosition.z);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject, transform2.parent);
      gameObject2.name = "arrow spr 2";
      gameObject2.transform.localPosition = new Vector3(gameObject2.transform.localPosition.x - 4f, gameObject2.transform.localPosition.y, gameObject2.transform.localPosition.z);
      GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject, transform2.parent);
      gameObject3.name = "arrow spr 3";
      gameObject3.transform.localPosition = new Vector3(gameObject3.transform.localPosition.x - 8f, gameObject3.transform.localPosition.y, gameObject3.transform.localPosition.z);
    }

    private static void SetMinimumAmount(ref CraftItemGUI CraftItemGUI) => MaxButtonCrafting.SetAmount(ref CraftItemGUI, 1);

    private static void SetMaximumAmount(
      ref CraftItemGUI CraftItemGUI,
      ref WorldGameObject Craftery_wgo)
    {
      int val1 = 9999;
      int val2_1 = 9999;
      int num1 = 0;
      MultiInventory multiInventory = !GlobalCraftControlGUI.is_global_control_active ? MainGame.me.player.GetMultiInventoryForInteraction() : GUIElements.me.craft.multi_inventory;
      for (int index = 0; index < CraftItemGUI.craft_definition.needs_from_wgo.Count; ++index)
      {
        Item obj = CraftItemGUI.craft_definition.needs_from_wgo[index];
        if (obj != null && (UnityEngine.Object) Craftery_wgo != (UnityEngine.Object) null && Craftery_wgo.data != null && obj.id == "fire" && obj.value > 0)
          val2_1 = Craftery_wgo.data.GetTotalCount(obj.id) / obj.value;
        if (val2_1 <= 1)
        {
          MaxButtonCrafting.SetAmount(ref CraftItemGUI, 1);
          return;
        }
      }
      for (int index = 0; index < CraftItemGUI.current_craft.needs.Count; ++index)
      {
        int num2 = 0;
        num1 = 0;
        Item need = CraftItemGUI.current_craft.needs[index];
        if (multiInventory != null)
          num2 += multiInventory.GetTotalCount(need.id);
        if (num2 == 0 || num2 < need.value)
        {
          MaxButtonCrafting.SetAmount(ref CraftItemGUI, 1);
          return;
        }
        int val2_2 = num2 / need.value;
        val1 = Math.Min(val1, val2_2);
      }
      int Amount = Math.Max(Math.Min(val1, val2_1), 1);
      MaxButtonCrafting.SetAmount(ref CraftItemGUI, Amount);
    }

    private static void SetAmount(ref CraftItemGUI CraftItemGUI, int Amount)
    {
      if (MaxButtonCrafting.fieldAmount == (FieldInfo) null)
        MaxButtonCrafting.fieldAmount = typeof (CraftItemGUI).GetField("_amount", AccessTools.all);
      MaxButtonCrafting.fieldAmount.SetValue((object) CraftItemGUI, (object) Amount);
      CraftItemGUI.Redraw();
      CraftItemGUI.OnOver();
    }
  }
}
