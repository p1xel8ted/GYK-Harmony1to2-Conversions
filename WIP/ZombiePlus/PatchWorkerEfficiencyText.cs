// Decompiled with JetBrains decompiler
// Type: ZombiePlus.PatchWorkerEfficiencyText
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Harmony;
using UnityEngine;

namespace ZombiePlus
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
      if (!GUIElements.me.autopsy.is_just_opened)
        str2 = string.Format("ID: {0} ({1})\nQuality:[(rskull){2}|(skull){3}|a{4}]\nSpeed: {5}\n", workerUniqueId, str1, num1, num2, num3, num4);
      else
        str2 = string.Format("ID: {0} ({1}) [(rskull){2}|(skull){3}|a{4}] [s{5}]", workerUniqueId, str1, num1, num2, num3, num4);
      __result = str2;
      return false;
    }
  }
}
