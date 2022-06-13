using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace AlchemyResearch
{
    public class MainPatcher
    {
        public static string ResultPreviewText = "Result";
        public static readonly string configFilePathAndName = "./QMods/AlchemyResearch/config.txt";
        public static readonly string KnownRecipesFilePathAndName = "./QMods/AlchemyResearch/Known Recipes.txt";
        public static readonly char ParameterSeparator = '|';
        public static readonly string ParameterComment = "#";
        public static readonly string ParameterSectionBegin = "[";
        public static readonly string ParameterSectionEnd = "]";

        public static void Patch()
        {
            var harmony = new Harmony("com.graveyardkeeper.urbanvibes.alchemyresearch");
            var assembly = Assembly.GetExecutingAssembly();
            harmony.PatchAll(assembly);
            Reflection.Initialization();
            ReadParametersFromFile();
        }

        public static void ReadParametersFromFile()
        {
            string[] strArray1;
            try
            {
                strArray1 = File.ReadAllLines(configFilePathAndName);
            }
            catch (Exception)
            {
                return;
            }

            if (strArray1 == null || strArray1.Length == 0)
                return;
            foreach (var str1 in strArray1)
            {
                var str2 = str1.Trim();
                if (!string.IsNullOrEmpty(str2) && !str2.StartsWith(ParameterComment))
                {
                    var strArray2 = str2.Split(ParameterSeparator);
                    if (strArray2.Length >= 2 && strArray2[0].Trim() == "ResultPreviewText" &&
                        !string.IsNullOrEmpty(strArray2[1].Trim()))
                    {
                        ResultPreviewText = strArray2[1].Trim();
                        break;
                    }
                }
            }
        }
    }
}