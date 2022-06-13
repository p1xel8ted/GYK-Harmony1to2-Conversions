// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchWorker
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (Worker))]
  [HarmonyPatch("UpdateWorkerLevel")]
  internal class PatchWorker
  {
    public static void writeLog(Worker __instance, string title)
    {
      if (!Entry.Config.Debug_Enabled)
        return;
      var num1 = __instance.worker_wgo.data.GetParam("working_k", 0.0f);
      var num2 = __instance.worker_wgo.data.GetParam("speed", 0.0f);
      int num3;
      int num4;
      int num5;
      __instance.worker_wgo.data.GetBodySkulls(ref num3, ref num4, ref num5, true);
      Entry.Log("=========[]> " + title);
      Entry.Log("Z_ID:" + __instance.worker_unique_id.ToString());
      Entry.Log("Z_Backpack:" + __instance.GetBackpack().GetItemName());
      Entry.Log("Is_worker:" + __instance.worker_wgo.data.is_worker.ToString());
      Entry.Log("Efficiency:" + num1.ToString() + ",");
      Entry.Log("Negative:" + num3.ToString() + ",");
      Entry.Log("Positive:" + num4.ToString() + ",");
      Entry.Log("Available:" + num5.ToString() + ",");
      Entry.Log("Speed:" + num2.ToString() + ",");
    }

    [HarmonyPostfix]
    public static void PatchWorkerLevelPostfix(Worker __instance)
    {
      HelperWorker.SetWorkerSpeed(__instance);
      HelperWorker.SetWorkerEfficiency(__instance);
    }
  }
}
