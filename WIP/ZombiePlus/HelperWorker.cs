// Decompiled with JetBrains decompiler
// Type: ZombiePlus.HelperWorker
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using System;
using System.Collections.Generic;

namespace ZombiePlus
{
  public class HelperWorker
  {
    public static List<Worker> SavedWorkers;

    public static void UpdateWorkersWhitReloadFlag(ref bool flag) => Utilities.DelayerDelegateWithFlag(ref flag, HelperWorker.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (HelperWorker.\u003C\u003Ec.\u003C\u003E9__0_0 = new GJCommons.VoidDelegate((object) HelperWorker.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateWorkersWhitReloadFlag\u003Eb__0_0))));

    public static void UpdateWorkersWithReloadFlag(bool flag) => Utilities.DelayerDelegateWithFlag1(flag, HelperWorker.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (HelperWorker.\u003C\u003Ec.\u003C\u003E9__1_0 = new GJCommons.VoidDelegate((object) HelperWorker.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateWorkersWithReloadFlag\u003Eb__1_0))));

    public static void SetWorkerSpeed(Worker _worker)
    {
      try
      {
        _worker.worker_wgo.data.SetParam("speed", Entry.Config.Zombie_MovementSpeed);
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[SetWorkerSpeed] id={0} error {1}", _worker.worker_unique_id, ex));
      }
    }

    public static void SetWorkerEfficiency(Worker _worker)
    {
      try
      {
        var data = _worker.worker_wgo.data;
        int num1;
        int num2;
        int num3;
        _worker.worker_wgo.data.GetBodySkulls(ref num1, ref num2, ref num3, true);
        var num4 = Entry.Config.Zombie_BaseEfficiency * 100f / Entry.Config.Zombie_MaxEfficiency;
        var num5 = num2 + Entry.Config.Zombie_ExtraEfficiency;
        data.SetParam("working_k", num5 / num4);
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[SetWorkerSpeed] id={0} error {1}", _worker.worker_unique_id, ex));
      }
    }
  }
}
