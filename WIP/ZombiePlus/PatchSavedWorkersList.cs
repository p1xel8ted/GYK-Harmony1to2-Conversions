// Decompiled with JetBrains decompiler
// Type: ZombiePlus.PatchSavedWorkersList
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Harmony;
using System.Collections.Generic;

namespace ZombiePlus
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
