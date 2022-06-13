using System.Reflection;
using HarmonyLib;

namespace NotKeepersSpeed
{
  public class MainPatcher
  {
    public static void Patch()
    {
        var harmony = new Harmony("com.glieseg.notkeepersspeed.mod");
        var assembly = Assembly.GetExecutingAssembly();
        harmony.PatchAll(assembly);
        }
  }
}
