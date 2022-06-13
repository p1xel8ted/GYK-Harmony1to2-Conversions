using HarmonyLib;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "OnCraftPressed", new System.Type[] { })]
    public class MixedCraftGUI_OnCraftPressed
    {
        [HarmonyPrefix]
        public static void Patch(MixedCraftGUI __instance)
        {
            AlchemyRecipe.Initialize();
            if (!(bool) Reflection.MethodIsCraftAllowed.Invoke(__instance, new object[0]))
                return;
            var mixedCraftPresetGui = (MixedCraftPresetGUI) Reflection.FieldCurrentPreset.GetValue(__instance);
            var craftDefinition = (CraftDefinition) Reflection.MethodGetCraftDefinition.Invoke(__instance, new object[2]
            {
                false,
                null
            });
            if (craftDefinition == null)
                craftDefinition = (CraftDefinition) Reflection.MethodGetCraftDefinition.Invoke(__instance, new object[2]
                {
                    true,
                    null
                });
            if (craftDefinition == null || !craftDefinition.id.StartsWith("mix:mf_alchemy"))
                return;
            var selectedItems = mixedCraftPresetGui.GetSelectedItems();
            if (selectedItems.Count < 2)
                return;
            for (var index = 0; index < selectedItems.Count; ++index)
            {
                switch (index)
                {
                    case 0:
                        AlchemyRecipe.Ingredient1 = selectedItems[index].id;
                        break;
                    case 1:
                        AlchemyRecipe.Ingredient2 = selectedItems[index].id;
                        break;
                    case 2:
                        AlchemyRecipe.Ingredient3 = selectedItems[index].id;
                        break;
                }

                AlchemyRecipe.Result = craftDefinition.GetFirstRealOutput().id;
                AlchemyRecipe.WorkstationUnityId = __instance.GetCrafteryWGO().GetInstanceID();
                AlchemyRecipe.WorkstationObjectId = __instance.GetCrafteryWGO().obj_id;
            }

            Logg.Log(string.Format("Processed Recipe: {0}|{1}|{2} => {3} | WGO: {4} / {5}", AlchemyRecipe.Ingredient1,
                AlchemyRecipe.Ingredient2, AlchemyRecipe.Ingredient3, AlchemyRecipe.Result,
                AlchemyRecipe.WorkstationUnityId, AlchemyRecipe.WorkstationObjectId));
        }
    }
}