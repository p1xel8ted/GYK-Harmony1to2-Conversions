using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (InventoryGUI), "OnItemPressedItem", new System.Type[] {typeof (Item)})]
  public class InventoryGUI_OnItemPressedItem
  {
    [HarmonyPrefix]
    public static void Prefix() => MaxButtonVendor.SetCaller("InventoryGUI", false, 1, (Item) null, (Trading) null);
  }
}
