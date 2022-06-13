using System.Reflection;
using HarmonyLib;

namespace GKInflationModReverse
{
  internal class MainPatcher
  {
    public static void Patch()
    {
        var harmony = new Harmony("com.ithinkandicode.graveyardkeeper.inflationreverse.mod");
        var assembly = Assembly.GetExecutingAssembly();
        harmony.PatchAll(assembly);
    }
  }
}
