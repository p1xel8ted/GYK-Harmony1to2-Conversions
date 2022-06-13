using HarmonyLib;
using UnityEngine;

namespace NotKeepersSpeed
{
  [HarmonyPatch(typeof (MovementComponent))]
  [HarmonyPatch("UpdateMovement")]
  internal class MovementComponent_UpdateMovement_Patch
  {
    [HarmonyPrefix]
    public static bool Prefix(MovementComponent __instance, Vector2 dir, ref float delta_time)
    {
      if (!__instance.wgo.is_player || __instance.player_controlled_by_script || __instance.wgo.is_dead)
        return true;
      Config.Options options = Config.GetOptions();
      if ((double) __instance.wgo.data.GetParam("speed") > 0.0)
      {
        float num1 = 3.3f + __instance.wgo.data.GetParam("speed_buff");
        float num2 = delta_time * options.EnergyForSprint;
        if ((options.SprintToggle ? (options.SprintKey.IsToggled() ? 1 : 0) : (Input.GetKey(options.SprintKey.Key) ? 1 : 0)) != 0 && (double) MainGame.me.player.energy >= (double) num2)
        {
          __instance.SetSpeed(num1 * options.SprintSpeed);
          if ((double) num2 > 0.0)
            MainGame.me.player.energy -= num2;
        }
        else
          __instance.SetSpeed(num1 * options.DefaultSpeed);
      }
      return true;
    }
  }
}
