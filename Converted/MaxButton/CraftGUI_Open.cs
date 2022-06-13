using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (CraftGUI), "Open", new System.Type[] {typeof (WorldGameObject), typeof (CraftsInventory), typeof (string)})]
  public class CraftGUI_Open
  {
    [HarmonyPostfix]
    public static void Postfix(CraftGUI __instance, WorldGameObject craftery_wgo)
    {
      if (LazyInput.gamepad_active)
        return;
      CraftItemGUI[] componentsInChildren = __instance.GetComponentsInChildren<CraftItemGUI>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        MaxButtonCrafting.AddMinAndMaxButtons(componentsInChildren[index], "amount btn R", "amount btn max", true, craftery_wgo);
        MaxButtonCrafting.AddMinAndMaxButtons(componentsInChildren[index], "amount btn L", "amount btn min", false, craftery_wgo);
      }
    }
  }
}
