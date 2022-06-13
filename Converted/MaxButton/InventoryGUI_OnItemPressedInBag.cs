using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (InventoryGUI), "OnItemPressedInBag", new System.Type[] {})]
  public class InventoryGUI_OnItemPressedInBag
  {
    [HarmonyPrefix]
    public static void Prefix() => MaxButtonVendor.SetCaller("InventoryGUI", false, 1, (Item) null, (Trading) null);
  }
}
