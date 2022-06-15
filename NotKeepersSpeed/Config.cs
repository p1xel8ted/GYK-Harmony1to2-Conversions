using System.IO;
using UnityEngine;

namespace NotKeepersSpeed
{
    public static class Config
    {
        private static Options _options;

        public static Options GetOptions() => GetOptions(false);

        public static void Log(string line) => File.AppendAllText("./QMods/NotKeepersSpeed/log.txt", line);

        private static Options GetOptions(bool forceReload)
        {
            if (_options != null)
                return _options;
            _options = new Options();
            const string path = "./QMods/NotKeepersSpeed/config.txt";
            if (!File.Exists(path)) return _options;
            foreach (var readAllLine in File.ReadAllLines(path))
            {
                if (readAllLine.Length < 3 || readAllLine[0] == '#') continue;
                var strArray = readAllLine.Split('=');
                if (strArray.Length <= 1) continue;
                var str = strArray[0];
                var raw = strArray[1];
                if (str != "SprintSpeed")
                {
                    if (str != "DefaultSpeed")
                    {
                        if (str != "EnergyForSprint")
                        {
                            if (str != "SprintKey")
                            {
                                if (str == "SprintToggle")
                                    _options.SprintToggle = ParseBool(raw);
                            }
                            else
                            {
                                try
                                {
                                    _options.SprintKey.ChangeKey(Enum<KeyCode>.Parse(raw));
                                }
                                catch
                                {
                                    // ignored
                                }
                            }
                        }
                        else
                            _options.EnergyForSprint = ParseNonNegative(raw, _options.EnergyForSprint);
                    }
                    else
                        _options.DefaultSpeed = ParsePositive(raw, _options.DefaultSpeed);
                }
                else
                    _options.SprintSpeed = ParsePositive(raw, _options.SprintSpeed);
            }
            return _options;
        }

        private static bool ParseBool(string raw) => raw == "1" || raw.ToLower() == "true";

        private static float ParseFloat(string raw, float @default)
        {
            return float.TryParse(raw, out var result) ? result : @default;
        }

        private static float ParseFloat(string raw, float @default, float threshold)
        {
            var num = ParseFloat(raw, @default);
            return num > (double)threshold ? num : @default;
        }

        private static float ParseNonNegative(string raw, float @default)
        {
            var num = ParseFloat(raw, @default);
            return num >= 0.0 ? num : 0.0f;
        }

        private static float ParsePositive(string raw, float @default) => ParseFloat(raw, @default, 0.0f);

        public class Options
        {
            public readonly ToggleKey SprintKey = new ToggleKey(KeyCode.LeftShift);
            public float DefaultSpeed = 1f;
            public float EnergyForSprint;
            public float SprintSpeed = 2f;
            public bool SprintToggle;
        }

        public class SinglePressKey
        {
            protected SinglePressKey(KeyCode key) => Key = key;

            public KeyCode Key { get; private set; }

            public void ChangeKey(KeyCode key) => Key = key;

            protected virtual bool IsPressed() => false;
        }

        public class ToggleKey : SinglePressKey
        {
            private bool _toggled;

            public ToggleKey(KeyCode key)
              : base(key)
            {
            }

            public bool IsToggled()
            {
                IsPressed();
                return _toggled;
            }

            protected override bool IsPressed()
            {
                if (!base.IsPressed())
                    return false;
                _toggled = !_toggled;
                return true;
            }
        }
    }
}