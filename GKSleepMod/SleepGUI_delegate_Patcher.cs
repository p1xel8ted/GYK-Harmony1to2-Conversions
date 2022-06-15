using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace GKSleepMod
{
    [HarmonyPatch(typeof(SleepGUI), nameof(SleepGUI.Open))]
    internal class SleepGuiDelegatePatcher
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> instructions)
        {
            Debug.Log("Starting transpiler");
            const float num1 = 50f;
            const float num2 = 0.4166667f;
            var index1 = 0;
            var index2 = 0;
            var source = new List<CodeInstruction>(instructions);
            for (var index3 = 1; index3 < source.Count; ++index3)
            {
                if (source[index3].operand == null) continue;
                if (source[index3].opcode == OpCodes.Call &&
                    source[index3].operand.ToString() == "Void set_fixedDeltaTime(Single)" &&
                    source[index3 - 1].opcode == OpCodes.Ldc_R4)
                {
                    index2 = index3 - 1;
                    Debug.Log("Found fixedDeltaTime");
                }
                else if (source[index3].opcode == OpCodes.Call &&
                         source[index3].operand.ToString() == "Void set_timeScale(Single)" &&
                         source[index3 - 1].opcode == OpCodes.Ldc_R4)
                {
                    index1 = index3 - 1;
                    Debug.Log("Found timeScale");
                }
            }

            Debug.Log("Finished searching");
            source[index2].operand = num2;
            source[index1].operand = num1;
            Debug.Log("Done with transpiler");
            return source.AsEnumerable();
        }
    }
}