using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MaxButton
{
  public class MaxButtonVendor
  {
    public static string MaxButtonName = "Max";
    private static MethodInfo methodSliderSetValue = (MethodInfo) null;
    private static string caller = string.Empty;
    private static bool fromPlayer = false;
    private static int maximumQuantity = 1;
    private static Item item = (Item) null;
    private static Trading trading = (Trading) null;
    private static ItemCountGUI.PriceCalculateDelegate priceCalculator = (ItemCountGUI.PriceCalculateDelegate) null;

    public static void SetCaller(
      string Caller,
      bool FromPlayer,
      int MaximumQuantity,
      Item Item,
      Trading Trading)
    {
      MaxButtonVendor.caller = Caller;
      MaxButtonVendor.fromPlayer = FromPlayer;
      MaxButtonVendor.maximumQuantity = MaximumQuantity;
      MaxButtonVendor.item = Item;
      MaxButtonVendor.trading = Trading;
    }

    public static void SetPriceCalculator(
      ItemCountGUI.PriceCalculateDelegate PriceCalculateDelegate)
    {
      MaxButtonVendor.priceCalculator = PriceCalculateDelegate;
    }

    public static void AddMaxButton(
      ref ItemCountGUI ItemCountGUI,
      ItemCountGUI.PriceCalculateDelegate PriceCalculateDelegate)
    {
      MaxButtonVendor.SetPriceCalculator(PriceCalculateDelegate);
      Transform transform1 = ItemCountGUI.transform.Find("window/dialog buttons/buttons table/button 1");
      Transform transform2 = ItemCountGUI.transform.Find("window/dialog buttons/buttons table/button max");
      if (LazyInput.gamepad_active)
      {
        if (!((UnityEngine.Object) transform2 != (UnityEngine.Object) null))
          return;
        transform2.gameObject.SetActive(false);
      }
      else if (!MaxButtonVendor.caller.Equals("VendorGUI"))
      {
        if (!((UnityEngine.Object) transform2 != (UnityEngine.Object) null))
          return;
        transform2.gameObject.SetActive(false);
      }
      else if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
      {
        transform2.gameObject.SetActive(true);
      }
      else
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform1.gameObject, transform1.parent);
        gameObject.name = "button max";
        UnityEngine.Object.Destroy((UnityEngine.Object) gameObject.GetComponent<DialogButtonGUI>());
        gameObject.GetComponent<UILabel>().text = MaxButtonVendor.MaxButtonName;
        UIButton uiButton = gameObject.AddComponent<UIButton>();
        gameObject.AddComponent<BoxCollider2D>().size = new Vector2(70f, 20f);
        SmartSlider slider = ItemCountGUI.transform.Find("window/Container/smart slider").GetComponent<SmartSlider>();
        uiButton.onClick = new List<EventDelegate>();
        EventDelegate.Add(uiButton.onClick, (EventDelegate.Callback) (() => MaxButtonVendor.SetMaxPrice(ref slider)));
      }
    }

    public static void SetMaxPrice(ref SmartSlider Slider)
    {
      int val1 = 0;
      if (!MaxButtonVendor.caller.Equals("VendorGUI"))
        return;
      float num1 = (!MaxButtonVendor.fromPlayer ? MaxButtonVendor.trading.player_money : MaxButtonVendor.trading.trader.cur_money) + MaxButtonVendor.trading.GetTotalBalance();
      for (int amount = 1; amount <= MaxButtonVendor.maximumQuantity; ++amount)
      {
        float num2 = MaxButtonVendor.priceCalculator(amount);
        if ((double) num1 >= (double) num2)
          ++val1;
        else
          break;
      }
      int Quantity = Math.Max(val1, 1);
      MaxButtonVendor.SetSliderValue(ref Slider, Quantity);
    }

    private static void SetSliderValue(ref SmartSlider Slider, int Quantity)
    {
      if (MaxButtonVendor.methodSliderSetValue == (MethodInfo) null)
        MaxButtonVendor.methodSliderSetValue = typeof (SmartSlider).GetMethod("SetValue", AccessTools.all);
      MaxButtonVendor.methodSliderSetValue.Invoke((object) Slider, new object[2]
      {
        (object) Quantity,
        (object) true
      });
    }
  }
}
