using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ZombieEnhanced
{
  public class HelperWorker
  {
    public static List<Worker> SavedWorkers;
    public static List<string> ZPorterStartDialogue = new List<string>()
    {
      "Go.. Go...!!",
      "Zombo Boooore!",
      "When.. Milord!?",
      "argghhhh again!?..",
      "eeet.. breens..",
      "yyoooyoo....",
      "Bbbbbbaabaaba am gooin...",
      ""
    };
    public static List<string> ZPorterEndDialogue = new List<string>()
    {
      "Fyuhh.. a..m zired!!",
      "aaa vring vomething voss!",
      "Brreens breens.. am heeer..",
      "Grhhhhhr.. whyyy??",
      "Zee yaa...",
      "Zere is morr..",
      ""
    };
    public static List<string> ZFinishCraftDialogue = new List<string>()
    {
      "Zork.. zork... Groo..",
      "Gorrdd Gorrdd...",
      "Iz dooon.. voss..",
      "Waaaaaa...",
      "Umm.."
    };
    public static List<string> ZCraftProgressDialogue = new List<string>()
    {
      "Haffway...",
      "Groo.. yee groo..",
      "Zombee zombee.. Sombe!",
      "In your heaaad..",
      "Arrmooz voss...",
      "Umm.."
    };
    public static List<string> ZCraftStartProgressDialogue = new List<string>()
    {
      "I Kravt fo you~",
      "Gimme yer head!!",
      "Zo good kraf, zo must krave..",
      "Kraf kraf kraf..",
      "Umm.."
    };
    public static List<string> ZPorterUpdateDialogue = new List<string>()
    {
      "Runnn"
    };

    public static void UpdateWorkersWithReloadFlag(ref bool flag) => Utilities.DelayerDelegateWithFlag(ref flag, HelperWorker.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (HelperWorker.\u003C\u003Ec.\u003C\u003E9__7_0 = new GJCommons.VoidDelegate((object) HelperWorker.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateWorkersWithReloadFlag\u003Eb__7_0))));

    public static void UpdateWorkersWithReloadFlag(bool flag) => Utilities.DelayerDelegateWithFlag(flag, HelperWorker.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (HelperWorker.\u003C\u003Ec.\u003C\u003E9__8_0 = new GJCommons.VoidDelegate((object) HelperWorker.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CUpdateWorkersWithReloadFlag\u003Eb__8_0))));

    public static IEnumerable<WaitForSeconds> WaitBeforeSaid()
    {
      yield return new WaitForSeconds(Time.time + (float) new Random().Next(0, 5));
    }

    public static void ZombieSaid(object str, long zombieId) => MainGame.me.save.workers.GetWorker(((IEnumerable<Worker>) SavedWorkers).FirstOrDefault<Worker>((Func<Worker, bool>) (w => w.worker_unique_id == zombieId)).worker_unique_id).worker_wgo.Say(string.Format("{0}", str), (GJCommons.VoidDelegate) null, new bool?(), (SpeechBubbleGUI.SpeechBubbleType) 0, (SmartSpeechEngine.VoiceID) 0, false);

    public static void ZombieSaid(List<string> dialogue, WorldGameObject worker, bool flag)
    {
      if (!flag || (double) (new Random().Next(100) * 100 / 100) >= (double) Entry.Config.Zombie_Dialogue_Chances)
        return;
      var waitForSeconds = new WaitForSeconds(Time.time + (float) new Random().Next(0, 3));
      int index = new Random().Next(0, dialogue.Count);
      worker.Say(dialogue[index] ?? "", (GJCommons.VoidDelegate) null, new bool?(), (SpeechBubbleGUI.SpeechBubbleType) 0, (SmartSpeechEngine.VoiceID) Entry.Config.Zombie_Dialogue_Voice, false);
    }

    public static void ZombieSaid(
      List<string> dialogue,
      WorldGameObject worker,
      bool flag,
      float percentage,
      float waitsec = 1f)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HelperWorker.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new HelperWorker.\u003C\u003Ec__DisplayClass12_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.percentage = percentage;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.dialogue = dialogue;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass120.worker = worker;
      if (!flag)
        return;
      // ISSUE: method pointer
      Utilities.DelayerDelegateWithFlag(flag, new GJCommons.VoidDelegate((object) cDisplayClass120, __methodptr(\u003CZombieSaid\u003Eb__0)), waitsec);
    }

    public static void ZombieSaid(
      string name,
      List<string> dialogue,
      WorldGameObject worker,
      bool flag,
      float percentage,
      float waitsec = 1f)
    {
      if (!Entry.Config.Zombie_Dialogue_Enabled || !flag)
        return;
      var realName = name;
      Utilities.DelayedExecute(realName, (Action<ZDialogs>) (zd =>
      {
        var num = new Random().Next(100) * 100 / 100;
        if ((double) num < (double) percentage)
        {
          int index = new Random().Next(dialogue.Count);
          zd.GetDialog(realName).DialogChances = (float) num;
          zd.GetDialog(realName).DialogChoosen = dialogue[index];
          worker.Say(dialogue[index] ?? "", (GJCommons.VoidDelegate) null, new bool?(), (SpeechBubbleGUI.SpeechBubbleType) 0, (SmartSpeechEngine.VoiceID) Entry.Config.Zombie_Dialogue_Voice, false);
        }
        zd.GetDialog(realName).DialogSuccess = (double) num < (double) percentage;
      }), waitsec);
    }

    public static void SetWorkerSpeed(Worker _worker)
    {
      try
      {
        _worker.worker_wgo.data.SetParam("speed", Entry.Config.Zombie_MovementSpeed);
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[SetWorkerSpeed] id={0} error {1}", (object) _worker.worker_unique_id, (object) ex));
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
        var num5 = (double) ((float) num2 + Entry.Config.Zombie_ExtraEfficiency) / (double) num4;
        data.SetParam("working_k", (float) num5);
      }
      catch (Exception ex)
      {
        Entry.Log(string.Format("[SetWorkerSpeed] id={0} error {1}", (object) _worker.worker_unique_id, (object) ex));
      }
    }
  }
}
