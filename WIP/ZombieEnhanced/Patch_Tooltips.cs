// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.Tooltip_Patcher
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System.Collections.Generic;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (ItemDefinition))]
  [HarmonyPatch("GetTooltipData")]
  public class Tooltip_Patcher
  {
    [HarmonyPostfix]
    private static void GetTooltipData_Postfix(
      ItemDefinition __instance,
      Item item,
      bool full_detail,
      List<BubbleWidgetData> __result)
    {
      if (!Entry.Config.Tooltip_Enabled || !((BalanceBaseObject) __instance).id.Contains("heart:heart") && !((BalanceBaseObject) __instance).id.Contains("intestine:intestine") && !((BalanceBaseObject) __instance).id.Contains("brain:brain") && !((BalanceBaseObject) __instance).id.Contains("heart:brain"))
        return;
      __result[0] = (BubbleWidgetData) new BubbleWidgetTextData(string.Format("{0}\n[(rskull){1}|(skull){2}]", (object) __instance.GetItemName(true), (object) __instance.q_minus, (object) __instance.q_plus), (UITextStyles.TextStyle) 3, (NGUIText.Alignment) 2, -1);
    }
  }
}
