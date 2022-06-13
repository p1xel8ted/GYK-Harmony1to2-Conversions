using System.Collections.Generic;

namespace AlchemyResearch
{
    public class ResearchedAlchemyRecipe
    {
        public string ingredient1 = "empty";
        public string ingredient2 = "empty";
        public string ingredient3 = "empty";
        public string result = "empty";

        public ResearchedAlchemyRecipe(
            string Ingredient1,
            string Ingredient2,
            string Ingredient3,
            string Result)
        {
            ingredient1 = Ingredient1;
            ingredient2 = Ingredient2;
            ingredient3 = Ingredient3;
            result = Result;
        }

        public string GetKey() => GetKey(ingredient1, ingredient2, ingredient3);

        public static string GetKey(string Ingredient1, string Ingredient2, string Ingredient3)
        {
            var stringList = new List<string>(3);
            stringList.Add(Ingredient1);
            stringList.Add(Ingredient2);
            stringList.Add(Ingredient3);
            stringList.Sort();
            return string.Format("{0}|{1}|{2}", stringList[0], stringList[1], stringList[2]);
        }

        public string AlchemyRecipeToString(
            string Ingredient1,
            string Ingredient2,
            string Ingredient3,
            string Result)
        {
            return string.Format("{0}|{1}", GetKey(Ingredient1, Ingredient2, Ingredient3), Result);
        }
    }
}