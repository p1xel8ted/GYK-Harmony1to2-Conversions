// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.IniConfig
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Oculus.Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ZombieEnhanced
{
  public class IniConfig
  {
    public string Path;
    public string defaultSection = "[Config]";
    public string modFolder = "QMods/ZombieEnhanced/";
    private string EXE = Assembly.GetExecutingAssembly().GetName().Name;
    private static readonly Lazy<IniConfig> _instance = new Lazy<IniConfig>((Func<IniConfig>) (() => new IniConfig()));

    public static IniConfig Instance => _instance.Value;

    private IniConfig(string IniPath = null)
    {
      var fileName = IniPath ?? modFolder + EXE + ".cfg";
      if (!Directory.Exists(modFolder))
        fileName = IniPath ?? EXE + ".cfg";
      Path = new FileInfo(fileName).FullName;
      Entry.Log("Path: " + Path);
    }

    public void WriteJsonConfig(Options configInstance) => Utilities.WriteToJSONFile<Options>(configInstance, "ZombieEnhanced", "cfg", "./");

    public Options ReadJsonConfig()
    {
      using (var streamReader = File.OpenText(Instance.Path))
        return JsonConvert.DeserializeObject<Options>(streamReader.ReadToEnd());
    }

    public void WriteConfigToFile(Options objConfig, bool useDot = false)
    {
      using (var text = File.CreateText(Instance.Path))
      {
        foreach (var property in objConfig.GetType().GetProperties())
          text.WriteLine(string.Format("{0}={1}", (object) property.Name, (object) property.GetValue((object) objConfig).ToString().Replace(',', '.')));
      }
    }

    public T DeserializeConfig<T>(string fileName, bool display = false)
    {
      var type = typeof (T);
      var instance = Activator.CreateInstance(type);
      foreach (var readLine in File.ReadLines(fileName))
      {
        var strArray = readLine.Split('=');
        if (strArray.Length == 2)
        {
          var property = type.GetProperty(strArray[0].Trim());
          var obj = Convert.ChangeType(!display ? (object) strArray[1] : (object) strArray[1].Replace(".", ","), property.PropertyType);
          Console.WriteLine(obj);
          if (property != (PropertyInfo) null)
            property.SetValue(instance, obj);
        }
      }
      return (T) instance;
    }
  }
}
