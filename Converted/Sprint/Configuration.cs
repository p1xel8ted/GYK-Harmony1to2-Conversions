using System;
using System.IO;

namespace Sprint
{
    public class Configuration
    {
        private static Configuration _configuration;
        private static bool _isToggleMode;
        public static float SprintCost = 0.05f;
        public static float SprintSpeed = 7f;

        public static Configuration GetInstance()
        {
            return _configuration ?? (_configuration = new Configuration());
        }

        private Configuration()
        {
            var streamReader = new StreamReader("./QMods/Sprint/config.txt");
            _isToggleMode = !streamReader.ReadLine().Split('=')[1].Equals("false");
            SprintCost = (float) Convert.ToDouble(streamReader.ReadLine().Split('=')[1]);
            SprintSpeed = (float) Convert.ToDouble(streamReader.ReadLine().Split('=')[1]);
        }

        public bool GetToggleMode() => _isToggleMode;
    }
}