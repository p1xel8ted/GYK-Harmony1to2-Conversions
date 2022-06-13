using System.IO;
using UnityEngine;

namespace NotKeepersSpeed
{
  public class Config
  {
    private static Config.Options options_;

    public static void Log(string line) => File.AppendAllText("./QMods/NotKeepersSpeed/log.txt", line);

    private static bool parseBool(string raw) => raw == "1" || raw.ToLower() == "true";

    private static float parseFloat(string raw, float _default)
    {
      float result = 0.0f;
      return float.TryParse(raw, out result) ? result : _default;
    }

    private static float parseFloat(string raw, float _default, float threshold)
    {
      float num = Config.parseFloat(raw, _default);
      return (double) num > (double) threshold ? num : _default;
    }

    private static float parsePositive(string raw, float _default) => Config.parseFloat(raw, _default, 0.0f);

    private static float parseNonNegative(string raw, float _default)
    {
      float num = Config.parseFloat(raw, _default);
      return (double) num >= 0.0 ? num : 0.0f;
    }

    public static Config.Options GetOptions() => Config.GetOptions(false);

    public static Config.Options GetOptions(bool forceReload)
    {
      if (Config.options_ != null)
        return Config.options_;
      Config.options_ = new Config.Options();
      string path = "./QMods/NotKeepersSpeed/config.txt";
      if (File.Exists(path))
      {
        foreach (string readAllLine in File.ReadAllLines(path))
        {
          if (readAllLine.Length >= 3 && readAllLine[0] != '#')
          {
            string[] strArray = readAllLine.Split('=');
            if (strArray.Length > 1)
            {
              string str = strArray[0];
              string raw = strArray[1];
              if (!(str == "SprintSpeed"))
              {
                if (!(str == "DefaultSpeed"))
                {
                  if (!(str == "EnergyForSprint"))
                  {
                    if (!(str == "SprintKey"))
                    {
                      if (str == "SprintToggle")
                        Config.options_.SprintToggle = Config.parseBool(raw);
                    }
                    else
                    {
                      try
                      {
                        Config.options_.SprintKey.ChangeKey(Enum<KeyCode>.Parse(raw));
                      }
                      catch
                      {
                      }
                    }
                  }
                  else
                    Config.options_.EnergyForSprint = Config.parseNonNegative(raw, Config.options_.EnergyForSprint);
                }
                else
                  Config.options_.DefaultSpeed = Config.parsePositive(raw, Config.options_.DefaultSpeed);
              }
              else
                Config.options_.SprintSpeed = Config.parsePositive(raw, Config.options_.SprintSpeed);
            }
          }
        }
      }
      return Config.options_;
    }

    public class SinglePressKey
    {
      private KeyCode key_;
      private bool isPressed_;

      public KeyCode Key => this.key_;

      public bool AlreadyPressed => this.isPressed_;

      public SinglePressKey(KeyCode key) => this.key_ = key;

      public SinglePressKey(KeyCode key, bool alreadyPressed)
      {
        this.key_ = key;
        this.isPressed_ = true;
      }

      public void ChangeKey(KeyCode key) => this.key_ = key;

      public virtual bool IsPressed() => false;
    }

    public class ToggleKey : Config.SinglePressKey
    {
      private bool toggled_;

      public bool Toggled => this.toggled_;

      public ToggleKey(KeyCode key)
        : base(key)
      {
      }

      public bool IsToggled()
      {
        this.IsPressed();
        return this.toggled_;
      }

      public override bool IsPressed()
      {
        if (!base.IsPressed())
          return false;
        this.toggled_ = !this.toggled_;
        return true;
      }
    }

    public class SwitchKey : Config.SinglePressKey
    {
      private int state_;
      private int stateNum_ = 2;

      public int State => this.state_;

      public SwitchKey(KeyCode key)
        : base(key)
      {
      }

      public SwitchKey(KeyCode key, int statenum)
        : base(key)
      {
        this.stateNum_ = statenum;
      }

      public override bool IsPressed()
      {
        if (!base.IsPressed())
          return false;
        this.state_ = ++this.state_ % this.stateNum_;
        return true;
      }
    }

    public class Options
    {
      public float SprintSpeed = 2f;
      public float DefaultSpeed = 1f;
      public float EnergyForSprint;
      public bool SprintToggle;
      public Config.ToggleKey SprintKey = new Config.ToggleKey(KeyCode.LeftShift);
    }
  }
}
