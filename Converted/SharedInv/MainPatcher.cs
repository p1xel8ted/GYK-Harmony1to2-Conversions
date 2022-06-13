using System.Reflection;
using HarmonyLib;

namespace SharedInv
{
    public class MainPatcher
    {
        public static void Patch()
        {
            Helper.RemoveLogs();

            var harmony = new Harmony("com.roberts.graveyardkeeper.SharedInv.mod");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}