// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.Extra_Patcher
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (WorldGameObject))]
  [HarmonyPatch("CustomUpdate")]
  internal class Extra_Patcher
  {
    public static float executedTime = 1f;
    private static float timestamp = 0.0f;
    private static List<string> addedBuff;
    private static int justOnce = 0;
    private static bool haveRun = false;
    private static bool reload = false;

    private static string GetMethodName<T>(Expression<Action<T>> action)
    {
      if (!(action.Body is MethodCallExpression body))
        throw new ArgumentException("Only method calls are supported");
      return body.Method.Name;
    }

    private static void Prayer_Reset()
    {
      if (!Input.GetKeyDown(Entry.Config.Prayer_Key))
        return;
      if ((double) MainGame.me.player.GetParam("prayed_this_week", 0.0f) > 0.0)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        GUIElements.me.dialog.OpenYesNo("Reset Sermon ?", Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__7_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CPrayer_Reset\u003Eb__7_0))), (GJCommons.VoidDelegate) null, (GJCommons.VoidDelegate) null);
      }
      else
        GUIElements.me.dialog.OpenOK("Sermon Already Resetted", (GJCommons.VoidDelegate) null, "", false);
    }

    private static void Buffs_Activator() => Utilities.DelayerDelegateWithKey(Entry.Config.Buffs_Activator_Key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__8_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CBuffs_Activator\u003Eb__8_0))));

    private static void Buffs_Activator_remover() => Utilities.DelayerDelegateWithKey(Entry.Config.Buffs_Activator_Remover_Key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__9_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CBuffs_Activator_remover\u003Eb__9_0))));

    private static void Item_Spawner_Package_Body() => Utilities.DelayerDelegateWithKey(Entry.Config.Item_Spawner_Package_Key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__10_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__10_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CItem_Spawner_Package_Body\u003Eb__10_0))));

    private static void Item_Spawner_Logic() => Utilities.DelayerDelegateWithKey(Entry.Config.Item_Spawner_Key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__11_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__11_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CItem_Spawner_Logic\u003Eb__11_0))));

    private static void RunJustOnce()
    {
      if (justOnce != 0)
        return;
      if (Entry.Config.Item_Organs_Overhaul_Enabled)
        GameBalance.me.items_data.ForEach((Action<ItemDefinition>) (i =>
        {
          if (!((BalanceBaseObject) i).id.Contains("brain:brain") && !((BalanceBaseObject) i).id.Contains("intestine:intestine") && (!((BalanceBaseObject) i).id.Contains("heart:heart") || i.q_plus == 0))
            return;
          i.stack_count = Entry.Config.Item_Organs_Overhaul_StackCount;
          if (haveRun)
            return;
          i.q_plus += Entry.Config.Item_Organs_Additional_Value;
        }));
      if (Entry.Config.Item_Embalm_Overhaul_Enabled)
        GameBalance.me.items_data.ForEach((Action<ItemDefinition>) (i =>
        {
          new List<string>() { "embalm_-2_2" };
          if (!((BalanceBaseObject) i).id.Equals("embalm_-2_2") || haveRun)
            return;
          i.q_plus += Entry.Config.Item_Embalm_Additional_Value;
        }));
      HelperWorker.UpdateWorkersWithReloadFlag(!haveRun);
      if (Entry.Config.Debug_Enabled)
        MainGame.me.player.Say("ZE Overhaul Initiated", (GJCommons.VoidDelegate) null, new bool?(), (SpeechBubbleGUI.SpeechBubbleType) 0, (SmartSpeechEngine.VoiceID) 0, false);
      justOnce = 1;
      haveRun = true;
    }

    private static void Item_Spawner_Generator(string _key) => Utilities.DelayerDelegateWithKey(_key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__13_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__13_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CItem_Spawner_Generator\u003Eb__13_0))));

    private static void InGameReload() => Utilities.DelayerDelegateWithKey(Entry.Config.InGame_ReloadConfig_Key, Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__14_0 ?? (Extra_Patcher.\u003C\u003Ec.\u003C\u003E9__14_0 = new GJCommons.VoidDelegate((object) Extra_Patcher.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInGameReload\u003Eb__14_0))));

    [HarmonyPostfix]
    public static void PatchPostfix(WorldGameObject __instance, ref Item ____data)
    {
      InGameReload();
      if (Entry.Config.Prayer_Reset_Enabled)
        Prayer_Reset();
      if (Entry.Config.Buffs_Activator_Enabled)
        Buffs_Activator();
      Buffs_Activator_remover();
      Item_Spawner_Logic();
      Item_Spawner_Package_Body();
      RunJustOnce();
      HelperWorker.UpdateWorkersWithReloadFlag(ref reload);
    }
  }
}
