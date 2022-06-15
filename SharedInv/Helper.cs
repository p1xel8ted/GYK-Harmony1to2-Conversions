using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharedInv
{
    public static class Helper
    {
        private const string ConfigPath = "./QMods/SharedInv/config.json";
        private const string ErrorPath = "./QMods/SharedInv/error.txt";
        private const string LogPath = "./QMods/SharedInv/log.txt";

        public static Config GetConfig()
        {
            var config = new Config();
            try
            {
                if (!File.Exists(ConfigPath))
                    return config;
                using var reader1 = new StreamReader(ConfigPath);
                using (var reader2 = new JsonTextReader(reader1))
                {
                    config = JsonSerializer.CreateDefault().Deserialize<Config>(reader2);
                    reader2.Close();
                }

                reader1.Close();
            }
            catch (Exception ex)
            {
                Log($"Failed to load zones from config with error: {(object)ex.ToString()}",
                    true);
                return config;
            }

            return config;
        }

        public static void Log(string v, bool error = false)
        {
            using var streamWriter = File.AppendText(error ? ErrorPath : LogPath);
            streamWriter.WriteLine(v);
        }

        public static void RemoveLogs() => File.Delete(LogPath);

        public class Config
        {
            public bool Automatic { get; set; }
            public List<string> Excluded { get; set; }
            public bool IncludeCrafting { get; set; }
            public List<string> Included { get; set; }
            public bool IncludePlayer { get; set; }
            public bool LogZones { get; set; }
        }
    }
}