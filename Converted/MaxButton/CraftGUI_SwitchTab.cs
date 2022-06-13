using HarmonyLib;

namespace MaxButton
{
  [HarmonyPatch(typeof (CraftGUI), "SwitchTab", new System.Type[] {typeof (string)})]
  public class CraftGUI_SwitchTab
  {
    [HarmonyPostfix]
    public static void Postfix(CraftGUI __instance)
    {
      CraftItemGUI[] componentsInChildren = __instance.GetComponentsInChildren<CraftItemGUI>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        MaxButtonCrafting.AddMinAndMaxButtons(componentsInChildren[index], "amount btn R", "amount btn max", true, __instance.GetCrafteryWGO());
        MaxButtonCrafting.AddMinAndMaxButtons(componentsInChildren[index], "amount btn L", "amount btn min", false, __instance.GetCrafteryWGO());
      }
    }
  }
}
