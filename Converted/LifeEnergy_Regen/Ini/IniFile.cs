
using System;
using System.Collections.Generic;
using System.IO;

namespace Ini
{
    public class IniFile
    {
        private string _theFile;
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public IniFile(string file, string commentDelimiter = ";")
        {
            CommentDelimiter = commentDelimiter;
            TheFile = file;
        }

        public IniFile() => CommentDelimiter = ";";

        public string CommentDelimiter { get; set; }

        public string TheFile
        {
            get => _theFile;
            set
            {
                _theFile = null;
                _dictionary.Clear();
                if (!File.Exists(value))
                    return;
                _theFile = value;
                using (var streamReader = new StreamReader(_theFile))
                {
                    var str1 = "";
                    string str2;
                    while ((str2 = streamReader.ReadLine()) != null)
                    {
                        var str3 = str2.Trim();
                        if (str3.Length != 0 &&
                            (string.IsNullOrEmpty(CommentDelimiter) || !str3.StartsWith(CommentDelimiter)))
                        {
                            if (str3.StartsWith("[") && str3.Contains("]"))
                            {
                                var num = str3.IndexOf(']');
                                str1 = str3.Substring(1, num - 1).Trim();
                            }
                            else if (str3.Contains("="))
                            {
                                var length = str3.IndexOf('=');
                                var str4 = str3.Substring(0, length).Trim();
                                var str5 = str3.Substring(length + 1).Trim();
                                var lower = ("[" + str1 + "]" + str4).ToLower();
                                if (str5.StartsWith("\"") && str5.EndsWith("\""))
                                    str5 = str5.Substring(1, str5.Length - 2);
                                if (_dictionary.ContainsKey(lower))
                                {
                                    var num = 1;
                                    string key;
                                    do
                                    {
                                        key = string.Format("{0}~{1}", lower, ++num);
                                    } while (_dictionary.ContainsKey(key));

                                    _dictionary.Add(key, str5);
                                }
                                else
                                    _dictionary.Add(lower, str5);
                            }
                        }
                    }
                }
            }
        }

        private bool TryGetValue(string section, string key, out string value) =>
            _dictionary.TryGetValue((section.StartsWith("[") ? section + key : "[" + section + "]" + key).ToLower(),
                out value);

        public string GetValue(string section, string key, string defaultValue = "")
        {
            return !TryGetValue(section, key, out var str) ? defaultValue : str;
        }

        public string this[string section, string key] => GetValue(section, key);

        public int GetInteger(
            string section,
            string key,
            int defaultValue = 0,
            int minValue = int.MinValue,
            int maxValue = int.MaxValue)
        {
            if (!TryGetValue(section, key, out var s))
                return defaultValue;
            if (!int.TryParse(s, out var result1))
            {
                if (!double.TryParse(s, out var result2))
                    return defaultValue;
                result1 = (int) result2;
            }

            if (result1 < minValue)
                result1 = minValue;
            if (result1 > maxValue)
                result1 = maxValue;
            return result1;
        }

        public double GetDouble(
            string section,
            string key,
            double defaultValue = 0.0,
            double minValue = double.MinValue,
            double maxValue = double.MaxValue)
        {
            if (!TryGetValue(section, key, out var s) || !double.TryParse(s, out var result))
                return defaultValue;
            if (result < minValue)
                result = minValue;
            if (result > maxValue)
                result = maxValue;
            return result;
        }

        public bool GetBoolean(string section, string key, bool defaultValue = false)
        {
            return !TryGetValue(section, key, out var str) ? defaultValue : str != "0" && !str.StartsWith("f", true, null);
        }

        public string[] GetAllValues(string section, string key)
        {
            var key1 = section.StartsWith("[") ? (section + key).ToLower() : ("[" + section + "]" + key).ToLower();
            if (!_dictionary.TryGetValue(key1, out var str))
                return null;
            var stringList = new List<string>
            {
                str
            };
            var num = 1;
            while (true)
            {
                if (_dictionary.TryGetValue(string.Format("{0}~{1}", key1, ++num), out str))
                    stringList.Add(str);
                else
                    break;
            }

            return stringList.ToArray();
        }
    }
}