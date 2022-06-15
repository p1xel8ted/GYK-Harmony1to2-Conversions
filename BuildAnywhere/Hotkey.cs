using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuildAnywhere
{
    public class Hotkey
    {
        private static readonly List<KeyState> Listener = new List<KeyState>();

        public static event EventHandler<KeyCode> Keypress;

        public static void Update()
        {
            while (true)
            {
                if (!MainGame.paused && MainGame.game_started)
                {
                    foreach (var keyState in Listener)
                    {
                        Debug.Log("[BuildAnywhere] Checking for button: " + keyState.Key);
                        if (Input.GetKey(keyState.Key))
                            Debug.Log("[BuildAnywhere] Button pressed");
                        if (Input.GetKey(keyState.Key) == keyState.State) continue;
                        Debug.Log("[BuildAnywhere] State triggered");
                        keyState.State = !keyState.State;
                        if (keyState.State) continue;
                        var keypress = Keypress;
                        keypress?.Invoke(null, keyState.Key);
                    }
                }
                else
                {
                    foreach (var keyState in Listener)
                        keyState.State = false;
                }
                Thread.Sleep(50);
            }
        }

        private class Press : EventArgs
        {
            private readonly KeyCode _key;

            public Press(KeyCode key) => _key = key;
        }

        private class KeyState
        {
            public readonly KeyCode Key;
            public bool State;

            public KeyState(KeyCode key)
            {
                Key = key;
                State = false;
            }

            public override bool Equals(object obj)
            {
                var nullable = obj is KeyState keyState ? keyState.Key : new KeyCode?();
                return Key == nullable.GetValueOrDefault() & nullable.HasValue;
            }

            public override int GetHashCode() => (int)Key;
        }

        public static class Patch_test1
        {
            public static void Prefix(string uscript_name) => Debug.Log("HELLO WORLD: " + uscript_name);
        }
    }
}