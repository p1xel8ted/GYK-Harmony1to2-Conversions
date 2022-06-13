using System.Reflection;
using HarmonyLib;

namespace LifeEnergy_Regen
{
    public class Main
    {
        public static void Patch()
        {
            var harmony = new Harmony("com.eurion.lifeenergyregen");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
        }
    }
}