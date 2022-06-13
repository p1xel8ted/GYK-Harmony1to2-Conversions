// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchWorkerEfficiencyText
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using UnityEngine;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (Worker))]
  [HarmonyPatch("GetWorkerEfficiencyText")]
  internal class PatchWorkerEfficiencyText
  {
    [HarmonyPrefix]
    private static bool PatchPrefix(Worker __instance, ref string __result)
    {
      __instance.UpdateWorkerLevel();
      int num1;
      int num2;
      int num3;
      __instance.worker_wgo.data.GetBodySkulls(ref num1, ref num2, ref num3, true);
      var num4 = __instance.worker_wgo.data.GetParam("speed", 0.0f);
      var str1 = Mathf.RoundToInt(__instance.worker_wgo.data.GetParam("working_k", 0.0f) * 100f).ToString() + "%";
      var workerUniqueId = __instance.worker_wgo.worker_unique_id;
      string str2;
      if (!((BaseGUI) GUIElements.me.autopsy).is_just_opened)
        str2 = string.Format("ID: {0} ({1})\nQuality:[(rskull){2}|(skull){3}|a{4}]\nSpeed: {5}\n", (object) workerUniqueId, (object) str1, (object) num1, (object) num2, (object) num3, (object) num4);
      else
        str2 = string.Format("ID: {0} ({1}) [(rskull){2}|(skull){3}|a{4}] [s{5}]", (object) workerUniqueId, (object) str1, (object) num1, (object) num2, (object) num3, (object) num4);
      __result = str2;
      return false;
    }
  }
}
