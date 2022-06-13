using HarmonyLib;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(PlatformSpecific), "LoadGame",
        new System.Type[] {typeof(SaveSlotData), typeof(PlatformSpecific.OnGameLoadedDelegate)})]
    public class PlatformSpecific_LoadGame
    {
        [HarmonyPostfix]
        public static void Patch(SaveSlotData slot, PlatformSpecific.OnGameLoadedDelegate on_lodaded)
        {
            using (var enumerator = ResearchedAlchemyRecipes.ReadRecipesFromFile()[slot.filename_no_extension]
                       .GetEnumerator())
            {
                while (enumerator.MoveNext())
                    ResearchedAlchemyRecipes.ResearchedRecipes.Add(enumerator.Current.Key, enumerator.Current.Value);
            }
        }
    }
}