using System;
using System.Globalization;
using System.IO;

namespace Sprint
{
    public class Configuration
    {
        public static float SprintCost = 0.05f;
        public static float SprintSpeed = 7f;
        private static Configuration _configuration;
        private static bool _isToggleMode;

        private Configuration()
        {
            var streamReader = new StreamReader("./QMods/Sprint/config.txt");
            _isToggleMode = !streamReader.ReadLine().Split('=')[1].Equals("false");
            SprintCost = (float)Convert.ToDouble(streamReader.ReadLine().Split('=')[1], CultureInfo.InvariantCulture);
            SprintSpeed = (float)Convert.ToDouble(streamReader.ReadLine().Split('=')[1], CultureInfo.InvariantCulture);
        }

        public static Configuration GetInstance()
        {
            return _configuration ?? (_configuration = new Configuration());
        }

        public bool GetToggleMode() => _isToggleMode;
    }
}