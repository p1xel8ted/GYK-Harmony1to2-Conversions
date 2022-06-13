using HarmonyLib;
using UnityEngine;

namespace BuildAnywhere
{
  [HarmonyPatch(typeof (FloatingWorldGameObject), "RecalculateAvailability", null)]
  public class PatchIgnoreOverlap
  {
    public static bool Prepare() => Settings.i._ButtonIgnoreBuildOverlap > 0;

    public static void Postfix()
    {
      if (!Input.GetKey(Settings.i._ButtonIgnoreBuildOverlap))
        return;
      FloatingWorldGameObject.can_be_built = true;
    }
  }
}
