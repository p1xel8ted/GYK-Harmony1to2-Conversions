// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.PatchUpdate
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieEnhanced
{
  [HarmonyPatch(typeof (PorterStation))]
  [HarmonyPatch("Update")]
  internal class PatchUpdate
  {
    [HarmonyPostfix]
    public static void PatchUpdate_Postfix(
      PorterStation __instance,
      ref WorldGameObject ____wgo,
      ref WorldZone ____destination,
      ref WorldZone ____source)
    {
      if (!__instance.has_linked_worker || !__instance.is_correctly_inited || __instance.state != 2 && __instance.state != 3)
        return;
      var str = ____wgo.linked_worker.unique_id.ToString();
      HelperWorker.ZombieSaid(str ?? "", new List<string>()
      {
        "Vroom to " + string.Join(" ", ((Object) ((Component) ____destination).gameObject).name.Split('_')),
        "Vroom to " + string.Join(" ", ____destination.id.Split('_')),
        "Vroom vroom zel go!...",
        "'Dear girls, We like you for your brains, not your body. Sincerely, Zombies.'",
        "'A mind is a terrible thing to waste'",
        "Chop~ Chop~ Chop~",
        "Me eet breens, ye ar safe.",
        "Vroom.. zel to go mang!...",
        "Vroom to " + string.Join(" ", ((Object) ____destination).name.Split('_'))
      }, ____wgo.linked_worker, __instance.state == 2, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
      HelperWorker.ZombieSaid(str ?? "", new List<string>()
      {
        "I vroom " + string.Join(" ", ((Object) ((Component) ____destination).gameObject).name.Split('_')),
        "Vroom to " + string.Join(" ", ____source.id.Split('_')),
        "Vroom vroom.. zomb wan sleep...",
        "'Dear girls, We like you for your brains, not your body. Sincerely, Zombies.'",
        "'A mind is a terrible thing to waste'",
        "Chop~ Chop~ Chop~",
        "Me eet breens, ye ar safe.",
        "Vroom.. zel to go mang!...",
        "Vroom.. zomb need sleep mang...",
        "Zooin to " + string.Join(" ", ((Object) ____source).name.Split('_'))
      }, ____wgo.linked_worker, __instance.state == 3, Entry.Config.Zombie_Dialogue_Chances, Entry.Config.Zombie_Dialogue_WaitSec);
    }
  }
}
