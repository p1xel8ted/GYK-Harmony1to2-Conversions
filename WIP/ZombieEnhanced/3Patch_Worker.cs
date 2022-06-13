// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchSavedWorkersList
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System.Collections.Generic;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (SavedWorkersList))]
  [HarmonyPatch("GetWorker")]
  internal class PatchSavedWorkersList
  {
    [HarmonyPostfix]
    private static void PatchPostfix(
      SavedWorkersList __instance,
      ref long worker_unique_id,
      ref List<Worker> ____workers)
    {
      HelperWorker.SavedWorkers = ____workers;
    }
  }
}
