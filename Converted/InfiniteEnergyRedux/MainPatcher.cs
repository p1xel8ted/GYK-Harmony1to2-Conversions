using System.Reflection;
using HarmonyLib;

namespace com.deathpax.mods.GraveyardKeeper.InfiniteEnergyRedux
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.deathpax.mods.graveyardkeeper.infiniteenergyredux");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}