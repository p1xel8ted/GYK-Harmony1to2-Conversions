using HarmonyLib;
using UnityEngine;

namespace TestMod
{
    [HarmonyPatch(typeof(PlayerComponent))]
    [HarmonyPatch("Update")]
    internal class PlayerComponentUpdatePatch
    {
        public static bool InfiniteEnabled = true;

        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (Input.GetKey((KeyCode) 108))
                InfiniteEnabled = !InfiniteEnabled;
            if (InfiniteEnabled)
                MainGame.me.player.energy = MainGame.me.save.max_energy - 1f;
            return true;
        }
    }
}