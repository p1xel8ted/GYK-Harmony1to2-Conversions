using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (ChestGUI), "OnItemSelect", new System.Type[] {})]
  public class ChestGUI_OnItemSelect
  {
    [HarmonyPostfix]
    public static void Prefix() => MaxButtonVendor.SetCaller("ChestGUI", false, 1, (Item) null, (Trading) null);
  }
}
