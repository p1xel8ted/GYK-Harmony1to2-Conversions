// Decompiled with JetBrains decompiler
// Type: ZombiePlus.Patch
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Harmony;
using System;
using System.Linq.Expressions;

namespace ZombiePlus
{
  [HarmonyPatch(typeof (WorldGameObject))]
  [HarmonyPatch("CustomUpdate")]
  internal class Patch
  {
    public static float executedTime = 1f;
    private static float timestamp = 0.0f;
    private static int justOnce = 0;
    private static bool haveRun = false;
    private static bool reload = false;

    private static string GetMethodName<T>(Expression<Action<T>> action)
    {
      if (!(action.Body is MethodCallExpression body))
        throw new ArgumentException("Only method calls are supported");
      return body.Method.Name;
    }

    private static void RunJustOnce()
    {
      if (justOnce != 0)
        return;
      HelperWorker.UpdateWorkersWithReloadFlag(haveRun);
      justOnce = 1;
      haveRun = true;
    }

    [HarmonyPostfix]
    public static void PatchPostfix(WorldGameObject __instance, ref Item ____data)
    {
      InGameReload();
      RunJustOnce();
      HelperWorker.UpdateWorkersWhitReloadFlag(ref reload);
    }

    private static void InGameReload() => Utilities.DelayerDelegateWithKey(Entry.Config.InGame_ReloadConfig_Key, Patch.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (Patch.\u003C\u003Ec.\u003C\u003E9__3_0 = new GJCommons.VoidDelegate((object) Patch.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInGameReload\u003Eb__3_0))));
  }
}
