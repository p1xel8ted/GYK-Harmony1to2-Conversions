using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuildAnywhere
{
  public class Hotkey
  {
    public static List<KeyState> Listener = new List<KeyState>();

    public static event EventHandler<KeyCode> Keypress;

    public static void Update()
    {
      while (true)
      {
        if (!MainGame.paused && MainGame.game_started)
        {
          foreach (var keyState in Listener)
          {
            Debug.Log("[BuildAnywhere] Checking for button: " + keyState.Key.ToString());
            if (Input.GetKey(keyState.Key))
              Debug.Log("[BuildAnywhere] Button pressed");
            if (Input.GetKey(keyState.Key) != keyState.State)
            {
              Debug.Log("[BuildAnywhere] State triggered");
              keyState.State = !keyState.State;
              if (!keyState.State)
              {
                var keypress = Keypress;
                if (keypress != null)
                  keypress(null, keyState.Key);
              }
            }
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
      public KeyCode Key;

      public Press(KeyCode Key) => this.Key = Key;
    }

    public class KeyState
    {
      public KeyCode Key;
      public bool State;

      public KeyState(KeyCode Key)
      {
        this.Key = Key;
        State = false;
      }

      public override bool Equals(object obj)
      {
        var key = Key;
        var nullable = obj is KeyState keyState ? new KeyCode?(keyState.Key) : new KeyCode?();
        return key == nullable.GetValueOrDefault() & nullable.HasValue;
      }

      public override int GetHashCode() => (int) Key;
    }

    public static class Patch_test1
    {
      public static void Prefix(string uscript_name) => Debug.Log("HELLO WORLD: " + uscript_name);
    }
  }
}
