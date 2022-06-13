// Decompiled with JetBrains decompiler
// Type: ZombieEnhanced.Utilities
// Assembly: ZombieEnhanced, Version=2.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4022B546-E585-46CC-BE5E-ED1509FBE325
// Assembly location: E:\Downloads\IDM-Chrome\ZombieEnhanced-24-2-2-0-1612137970\ZombieEnhanced.dll

using Oculus.Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZombieEnhanced
{
  public class Utilities
  {
    public static float executedTime = 1f;
    private static float timestamp = 0.0f;
    public static float waitTime = 4f;
    public static float timer = 0.0f;
    public static string deployedPath = ".\\QMods\\ZombieEnhanced\\";

    public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act)
    {
      if (string.IsNullOrEmpty(key) || !Input.GetKey(key) || (double) Time.time < (double) timestamp)
        return;
      act.Invoke();
      timestamp = Time.time + executedTime;
    }

    public static void DelayerDelegateWithFlag(ref bool flag, GJCommons.VoidDelegate act)
    {
      if (!flag || (double) Time.time < (double) timestamp)
        return;
      act.Invoke();
      timestamp = Time.time + executedTime;
      flag = false;
    }

    public static void DelayerDelegateWithFlag(bool flag, GJCommons.VoidDelegate act)
    {
      if (!flag || (double) Time.time < (double) timestamp)
        return;
      act.Invoke();
      timestamp = Time.time + executedTime;
      flag = false;
    }

    public static void DelayerDelegateWithFlag(
      bool flag,
      GJCommons.VoidDelegate act,
      float waitsec)
    {
      if (!flag)
        return;
      timer += Time.deltaTime;
      if ((double) timer <= (double) waitsec)
        return;
      act.Invoke();
      timer -= waitsec;
    }

    public static void DelayedExecute(string name, Action<ZDialogs> act, float waitsec)
    {
      var me = ZDialogs.me;
      if (!me.IsTimeToExecute(name, waitsec))
        return;
      act(me);
      me.Update(name, waitsec);
    }

    public static T CloneObject<T>(T obj, Action<T> act = null)
    {
      var obj1 = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject((object) obj));
      if (act != null)
        act(obj1);
      return obj1;
    }

    public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act, string text = null) => DelayerDelegateWithKey(key, new GJCommons.VoidDelegate((object) new Utilities.\u003C\u003Ec__DisplayClass11_0()
    {
      text = text,
      act = act
    }, __methodptr(\u003CDelayerDelegateWithKey\u003Eb__0)));

    public static void WriteToJSONFile<T>(List<T> its, string filename)
    {
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombieEnhanced\\" + filename + ".json", contents);
      Entry.Log(string.Format("[ZombieEnhanced] name={0} type={1} WriteToJsonFile={2}", (object) nameof (its), (object) its.GetType(), (object) filename));
    }

    public static void WriteToJSONFileDebug<T>(List<T> its, string filename)
    {
      if (!Entry.Config.Debug_Enabled)
        return;
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      var strArray = filename.Split('/');
      if (strArray.Length > 1)
        Directory.CreateDirectory(strArray[0]);
      File.WriteAllText(".\\QMods\\ZombieEnhanced\\" + filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(HashSet<T> its, string filename)
    {
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombieEnhanced\\" + filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename)
    {
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombieEnhanced\\" + filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename, string path = null)
    {
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(path == null ? deployedPath + filename + ".json" : filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename, string ext = "json", string path = null)
    {
      var contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(path == null ? deployedPath + filename + "." + ext : filename + "." + ext, contents);
    }

    public static void WriteDictToFile(Dictionary<string, IList> dic)
    {
      foreach (var keyValuePair in dic)
      {
        Entry.Log(string.Format("[{0}, {1}]", (object) keyValuePair.Key, (object) keyValuePair.Value));
        foreach (var obj in (IEnumerable) keyValuePair.Value)
          Entry.Log(string.Format("-[{0}]", obj));
      }
    }

    public static void WriteListToFile(IList<Item> list)
    {
      foreach (var obj in (IEnumerable<Item>) list)
        Entry.Log(string.Format("[{0}, {1}]", (object) obj.money, (object) obj));
    }
  }
}
