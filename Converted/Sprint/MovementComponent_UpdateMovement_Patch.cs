using HarmonyLib;
using UnityEngine;

namespace Sprint
{
    [HarmonyPatch(typeof(MovementComponent))]
    [HarmonyPatch("UpdateMovement")]
    internal class MovementComponentUpdateMovementPatch
    {
        [HarmonyPostfix]
        public static void Postfix(MovementComponent __instance)
        {
            if (Configuration.GetInstance().GetToggleMode())
            {
                if (!__instance.wgo.is_player)
                    return;
                if (MainGame.me.player.GetParam("isSprinting", 0.0f) == 1.0)
                {
                    if (MainGame.me.player.energy > 1.0)
                    {
                        __instance.SetSpeed(Configuration.SprintSpeed);
                        MainGame.me.player.energy -= Configuration.SprintCost;
                    }
                    else
                    {
                        __instance.SetSpeed(3f);
                        MainGame.me.player.SetParam("isSprinting", 0.0f);
                    }
                }
                else if (__instance.wgo.is_player)
                    __instance.SetSpeed(3f);
            }
            else
            {
                if (Input.GetKey((KeyCode) 304) && __instance.wgo.is_player && MainGame.me.player.energy > 1.0)
                {
                    __instance.SetSpeed(Configuration.SprintSpeed);
                    MainGame.me.player.energy -= Configuration.SprintCost;
                }

                if (!Input.GetKey((KeyCode) 304) && __instance.wgo.is_player)
                    __instance.SetSpeed(3f);
            }
        }
    }
}