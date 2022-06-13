using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (InventoryGUI), "OnPressedSelect", new System.Type[] {})]
  public class InventoryGUI_OnPressedSelect
  {
    [HarmonyPrefix]
    public static void Prefix() => MaxButtonVendor.SetCaller("InventoryGUI", false, 1, (Item) null, (Trading) null);
  }
}
