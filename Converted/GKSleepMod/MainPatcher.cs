using System.Reflection;
using HarmonyLib;

namespace GKSleepMod
{
    internal class MainPatcher
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.fluffiest.graveyardkeeper.fastsleep.mod");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}