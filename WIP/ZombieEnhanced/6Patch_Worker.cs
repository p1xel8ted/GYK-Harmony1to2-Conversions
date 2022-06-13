// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchTrySendPorter
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (PorterStation))]
  [HarmonyPatch("TrySendPorter")]
  internal class PatchTrySendPorter
  {
    [HarmonyPostfix]
    public static void PatchTrySendPorterPostfix(
      PorterStation __instance,
      ref WorldGameObject ____wgo)
    {
      if (__instance.state == 1 || __instance.state == null)
        HelperWorker.ZombieSaid(____wgo.linked_worker.unique_id.ToString(), HelperWorker.ZPorterStartDialogue, ____wgo.linked_worker, __instance.HasLinkedWorker(), Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
      ((MovementComponent) ____wgo.linked_worker.components.character).SetSpeed(Entry.Config.Zombie_MovementSpeed);
    }
  }
}
