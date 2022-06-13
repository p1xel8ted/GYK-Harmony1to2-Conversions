using HarmonyLib;
using UnityEngine;

namespace Sprint
{
    [HarmonyPatch(typeof(PlayerComponent))]
    [HarmonyPatch("Update")]
    internal class PlayerComponentUpdatePatch
    {
        [HarmonyPrefix]
        public static bool Prefix(PlayerComponent __instance)
        {
            if (Configuration.GetInstance().GetToggleMode() && Input.GetKeyDown((KeyCode) 304) &&
                __instance.wgo.is_player)
            {
                MainGame.me.player.SetParam("isSprinting",
                    MainGame.me.player.GetParam("isSprinting", 0.0f) != 1.0 ? 1f : 0.0f);
            }

            return true;
        }
    }
}