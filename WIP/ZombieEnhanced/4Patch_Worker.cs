// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchWorkerSkin
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System;
using System.Collections.Generic;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (Worker))]
  [HarmonyPatch("UpdateWorkerSkin")]
  [HarmonyPatch(new Type[] {typeof (Worker.WorkerActivity)})]
  internal class PatchWorkerSkin
  {
    [HarmonyPrefix]
    public static void Prefix(Worker __instance, ref Worker.WorkerActivity worker_activity)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      switch ((int) (Worker.WorkerActivity) ^(int&) ref worker_activity)
      {
        case 0:
          HelperWorker.ZombieSaid(new List<string>()
          {
            "zZzZz.."
          }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
          break;
        case 1:
          HelperWorker.ZombieSaid(new List<string>()
          {
            "I work fo breens..."
          }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
          break;
        case 2:
          HelperWorker.ZombieSaid(new List<string>()
          {
            "I zransport fo voss"
          }, __instance.worker_wgo, true, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
          break;
      }
    }
  }
}
