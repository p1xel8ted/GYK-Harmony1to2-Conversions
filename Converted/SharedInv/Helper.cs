using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharedInv
{
    public static class Helper
    {
        public static string configPath = ".\\QMods\\SharedInv\\config.json";
        public static string logPath = ".\\QMods\\SharedInv\\log.txt";
        public static string errorPath = ".\\QMods\\SharedInv\\error.txt";

        public static Helper.Config GetConfig()
        {
            Helper.Config config = new Helper.Config();
            try
            {
                if (!File.Exists(Helper.configPath))
                    return config;
                using (StreamReader reader1 = new StreamReader(Helper.configPath))
                {
                    using (JsonTextReader reader2 = new JsonTextReader((TextReader) reader1))
                    {
                        config = JsonSerializer.CreateDefault().Deserialize<Helper.Config>((JsonReader) reader2);
                        reader2.Close();
                    }

                    reader1.Close();
                }
            }
            catch (Exception ex)
            {
                Helper.Log(string.Format("Failed to load zones from config with error: {0}", (object) ex.ToString()),
                    true);
                return config;
            }

            return config;
        }

        public static void Log(string v, bool error = false)
        {
            using (StreamWriter streamWriter = File.AppendText(error ? Helper.errorPath : Helper.logPath))
                streamWriter.WriteLine(v);
        }

        public static void RemoveLogs() => File.Delete(Helper.logPath);

        public class Config
        {
            public bool includePlayer { get; set; }

            public bool includeCrafting { get; set; }

            public bool automatic { get; set; }

            public bool logZones { get; set; }

            public List<string> included { get; set; }

            public List<string> excluded { get; set; }
        }
    }
}