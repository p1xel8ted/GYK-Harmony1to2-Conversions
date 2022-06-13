using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch
{
    [HarmonyPatch(typeof(MixedCraftGUI), "OnResourcePickerClosed", new System.Type[] {typeof(Item)})]
    public class MixedCraftGUI_OnResourcePickerClosed
    {
        [HarmonyPostfix]
        public static void Patch(MixedCraftGUI __instance, Item item)
        {
            var objId = __instance.GetCrafteryWGO().obj_id;
            var crafteryTransform = MixedCraftGUI_OpenAsAlchemy.GetCrafteryTransform(__instance.transform, objId);
            var ResultPreview = __instance.transform.Find("ingredient container result");
            MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
            if (!(bool) (Object) crafteryTransform)
                return;
            var transform1 = crafteryTransform.transform.Find("ingredients/ingredient container/Base Item Cell");
            var transform2 = crafteryTransform.transform.Find("ingredients/ingredient container (1)/Base Item Cell");
            var transform3 = crafteryTransform.transform.Find("ingredients/ingredient container (2)/Base Item Cell");
            if (!(bool) (Object) transform1 || !(bool) (Object) transform2)
                return;
            var component1 = transform1.GetComponent<BaseItemCellGUI>();
            var component2 = transform2.GetComponent<BaseItemCellGUI>();
            var baseItemCellGui = (BaseItemCellGUI) null;
            if ((bool) (Object) transform3)
                baseItemCellGui = transform3.GetComponent<BaseItemCellGUI>();
            if (!(bool) (Object) component1 || !(bool) (Object) component2)
            {
                MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
            }
            else
            {
                var id1 = component1.item.id;
                var id2 = component2.item.id;
                var Ingredient3 = "empty";
                if ((bool) (Object) baseItemCellGui)
                    Ingredient3 = baseItemCellGui.item.id;
                if (id1 == "empty" || id2 == "empty" || Ingredient3 == "empty" && objId == "mf_alchemy_craft_03")
                {
                    MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(ResultPreview);
                }
                else
                {
                    var ItemID = ResearchedAlchemyRecipes.IsRecipeKnown(id1, id2, Ingredient3);
                    MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawItem(ResultPreview, ItemID);
                }
            }
        }
    }
}