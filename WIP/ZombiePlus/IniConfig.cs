// Decompiled with JetBrains decompiler
// Type: ZombiePlus.IniConfig
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Oculus.Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ZombiePlus
{
  public class IniConfig
  {
    public string Path;
    public string defaultSelection = "[Config]";
    public string modFolder = "QMods/ZombiePlus/";
    private static readonly Lazy<IniConfig> _instance = new Lazy<IniConfig>(() => new IniConfig());
    private readonly string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    public static IniConfig Instance => _instance.Value;

    private IniConfig(string IniPath = null)
    {
      var fileName = IniPath ?? modFolder + EXE + ".cfg";
      if (!Directory.Exists(modFolder))
        fileName = IniPath ?? EXE + ".cfg";
      Path = new FileInfo(fileName).FullName;
      Entry.Log("Path: " + Path);
    }

    public void WriteJsonConfig(Options configInstance) => Utilities.WriteToJSONFile<Options>(configInstance, "ZombiePlus", "cfg", "./");

    public Options ReadJsonConfig()
    {
      Options options;
      using (var streamReader = File.OpenText(Instance.Path))
        options = JsonConvert.DeserializeObject<Options>(streamReader.ReadToEnd());
      return options;
    }

    public void WriteConfigToFile(Options objConfig, bool useDot = false)
    {
      using (var text = File.CreateText(Instance.Path))
      {
        foreach (var property in objConfig.GetType().GetProperties())
          text.WriteLine(string.Format("{0}={1}", property.Name, property.GetValue(objConfig).ToString().Replace(',', '.')));
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
          var obj = Convert.ChangeType(!display ? strArray[1] : (object) strArray[1].Replace(".", ","), property.PropertyType);
          Console.WriteLine(obj);
          if (property != null)
            property.SetValue(instance, obj);
        }
      }
      return (T) instance;
    }
  }
}
