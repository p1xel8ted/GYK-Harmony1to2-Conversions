// Decompiled with JetBrains decompiler
// Type: ZombiePlus.PatchOnCameToDestination
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Harmony;

namespace ZombiePlus
{
  [HarmonyPatch(typeof (PorterStation))]
  [HarmonyPatch("OnCameToDestination")]
  internal class PatchOnCameToDestination
  {
    [HarmonyPostfix]
    public static void PatchOnCameToDestinationPostfix(
      PorterStation __instance,
      ref WorldGameObject ____wgo)
    {
      ____wgo.linked_worker.components.character.SetSpeed(Entry.Config.Zombie_MovementSpeed);
    }
  }
}
