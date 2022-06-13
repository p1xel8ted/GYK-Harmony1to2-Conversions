using HarmonyLib;
using UnityEngine;

namespace BuildAnywhere
{
  [HarmonyPatch(typeof (FlowGridCell), "IsInsideWorldZone", null)]
  public class PatchIgnoreBuildingZone
  {
    public static bool Prepare() => Settings.i._ButtonIgnoreBuildArea > 0;

    public static void Postfix(ref bool __result)
    {
      if (!Input.GetKey(Settings.i._ButtonIgnoreBuildArea))
        return;
      __result = true;
    }
  }
}
