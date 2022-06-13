using System.Reflection;
using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (VendorGUI), "OpenItemCountWidnow", new System.Type[] {typeof (string), typeof (int), typeof (bool), typeof (bool)})]
  public class VendorGUI_Open
  {
    private static FieldInfo fieldItem;

    [HarmonyPrefix]
    public static void Patch(
      VendorGUI __instance,
      string item_id,
      int can_move,
      bool from_inventory,
      bool from_player)
    {
      if (VendorGUI_Open.fieldItem == (FieldInfo) null)
        VendorGUI_Open.fieldItem = typeof (VendorGUI).GetField("_selected_item", AccessTools.all);
      Item obj = (Item) VendorGUI_Open.fieldItem.GetValue((object) __instance);
      MaxButtonVendor.SetCaller("VendorGUI", from_player, can_move, obj, __instance.trading);
    }
  }
}
