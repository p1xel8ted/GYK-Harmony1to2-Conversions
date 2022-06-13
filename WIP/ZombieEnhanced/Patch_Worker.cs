// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.ZDialogs
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZombieEnhanced
{
  public class ZDialogs
  {
    public static List<ZDialog> Dialogues = new List<ZDialog>();
    private static readonly Lazy<ZDialogs> _instance = new Lazy<ZDialogs>((Func<ZDialogs>) (() => new ZDialogs()));

    public static ZDialogs me => _instance.Value;

    public void Create(string name, float next)
    {
      var zdialog = new ZDialog();
      if (Dialogues.Exists((Predicate<ZDialog>) (s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase))))
        return;
      zdialog.DialogueName = name;
      zdialog.currentTime = Time.time;
      zdialog.nextExecutedTime = Time.time + next;
      Dialogues.Add(zdialog);
      Init(name, next);
    }

    public bool Init(string name, float next)
    {
      var flag = Dialogues.Exists((Predicate<ZDialog>) (s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase)));
      if (!flag)
      {
        Create(name, next);
        return flag;
      }
      GetDialog(name).currentTime = Time.time;
      return flag;
    }

    public ZDialog GetDialog(string name) => Dialogues.Single<ZDialog>((Func<ZDialog, bool>) (s => string.Equals(s.DialogueName, name, StringComparison.CurrentCultureIgnoreCase))) ?? (ZDialog) null;

    public void Update(string name, float next)
    {
      GetDialog(name).lastExecutedTime = Time.time;
      GetDialog(name).nextExecutedTime = Time.time + next;
    }

    public bool IsTimeToExecute(string name, float next)
    {
      if (Init(name, next))
      {
        Utilities.WriteToJSONFileDebug<ZDialog>(Dialogues, "debug/initPhase");
        var dialog = GetDialog(name);
        dialog.currentTime = Time.time;
        if ((double) dialog.currentTime >= (double) dialog.nextExecutedTime && (double) dialog.nextExecutedTime > (double) dialog.lastExecutedTime)
        {
          dialog.lastExecutedTime = Time.time;
          dialog.nextExecutedTime = dialog.currentTime + next;
          Utilities.WriteToJSONFileDebug<ZDialog>(Dialogues, "debug/executedPhase");
          return true;
        }
      }
      Utilities.WriteToJSONFileDebug<ZDialog>(Dialogues, "debug/initPhaseFailed");
      return false;
    }
  }
}
