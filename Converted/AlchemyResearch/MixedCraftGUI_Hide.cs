using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "Hide", new System.Type[] {typeof(bool)})]
    public class MixedCraftGUI_Hide
    {
        [HarmonyPostfix]
        public static void Postfix(MixedCraftGUI __instance)
        {
            var transform = __instance.transform.Find("ingredient container result");
            if (!(bool) (Object) transform || !(bool) (Object) transform.gameObject)
                return;
            Object.Destroy(transform.gameObject);
        }
    }
}