using HarmonyLib;
using System;
using System.Globalization;
using System.IO;

namespace FasterCraft
{
    [HarmonyPatch(typeof(CraftComponent))]
    [HarmonyPatch("DoAction")]
    public class CraftComponentPatcher
    {
        [HarmonyPrefix]
        public static void Prefix(ref float delta_time)
        {
            const string path = @"./QMods/FasterCraft/config.txt";

            var file = new StreamReader(path);
            var line = file.ReadLine();
            var options = line?.Split('=');

            delta_time *= (float)Convert.ToDouble(options?[1], CultureInfo.InvariantCulture);
        }
    }
}