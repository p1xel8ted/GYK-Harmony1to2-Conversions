using System.Reflection;
using HarmonyLib;

namespace Sprint
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.glibfire.graveyardkeeper.sprint.mod");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}