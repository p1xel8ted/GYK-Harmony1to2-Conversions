using System;
using HarmonyLib;

namespace FasterCraft
{
    [HarmonyPatch(typeof(CraftComponent))]
    [HarmonyPatch("DoAction")]
    public class CraftComponentPatcher
    {
        [HarmonyPrefix]
        public static void Prefix(ref float delta_time)
        {
                string line;
                string[] options;


                var path = @"./QMods/FasterCraft/config.txt";

                System.IO.StreamReader file = new System.IO.StreamReader(path);
                line = file.ReadLine();
                options = line.Split('=');

               delta_time *= (float)Convert.ToDouble(options[1]);

        }
    }
}
