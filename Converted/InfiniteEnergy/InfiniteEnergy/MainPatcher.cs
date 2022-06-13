using System.Reflection;
using HarmonyLib;

namespace InfiniteEnergy
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.glibfire.graveyardkeeper.infiniteenergy.mod");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}