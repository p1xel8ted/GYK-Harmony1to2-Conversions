using System.Reflection;
using HarmonyLib;

namespace GKInflationMod
{
  internal class MainPatcher
  {
    public static void Patch()
    {
        var harmony = new Harmony("com.fluffiest.graveyardkeeper.inflation.mod");
        var assembly = Assembly.GetExecutingAssembly();
        harmony.PatchAll(assembly);
        }
  }
}
