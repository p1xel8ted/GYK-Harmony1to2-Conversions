using System.Reflection;
using HarmonyLib;

namespace AlwaysShowExperienceBar
{
  public class MainPatcher
  {
    public static FieldInfo fieldStayShownTime;

    public static void Patch()
    {
        var harmony = new Harmony("com.graveyardkeeper.urbanvibes.alwaysshowexperiencebar");
      var assembly = Assembly.GetExecutingAssembly();
      harmony.PatchAll(assembly);
      MainPatcher.fieldStayShownTime = typeof(AnimatedGUIPanel).GetField("stay_shown_time", AccessTools.all);
      }
  }
}
