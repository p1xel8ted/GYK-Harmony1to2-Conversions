// Decompiled with JetBrains decompiler
// Type: ZombiePlus.Utilities
// Assembly: ZombiePlus, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BD1FC9C3-F067-4EFB-8476-BCA6458A2629
// Assembly location: E:\Downloads\IDM-Chrome\ZombiePlus-35-0-0-1-1636472842_2\ZombiePlus\ZombiePlus.dll

using Oculus.Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZombiePlus
{
  internal class Utilities
  {
    public static float executedTime = 1f;
    private static float timestamp = 0.0f;
    public static float waitTime = 4f;
    public static float timer = 0.0f;
    public static string deployedPath = ".\\QMods\\ZombiePlus\\";

    public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act)
    {
      if (string.IsNullOrEmpty(key) || !Input.GetKey(key) || Time.time < (double) timestamp)
        return;
      act.Invoke();
      timestamp = Time.time + executedTime;
    }

    public static void DelayerDelegateWithFlag(ref bool flag, GJCommons.VoidDelegate act)
    {
      if (!flag || Time.time < (double) timestamp)
        return;
      act.Invoke();
      timestamp = Time.time + executedTime;
      flag = false;
    }

    public static void DelayerDelegateWithFlag1(bool flag, GJCommons.VoidDelegate act)
    {
      if (!flag || Time.time < (double) timestamp)
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
      if (timer <= (double) waitsec)
        return;
      act.Invoke();
      timer -= waitsec;
    }

    public static void DelayerDelegateWithKey(string key, GJCommons.VoidDelegate act, string text = null) => DelayerDelegateWithKey(key, new GJCommons.VoidDelegate((object) new Utilities.\u003C\u003Ec__DisplayClass4_0()
    {
      text = text,
      act = act
    }, __methodptr(\u003CDelayerDelegateWithKey\u003Eb__0)));

    public static void WriteToJSONFile<T>(List<T> its, string filename)
    {
      string contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
      Entry.Log(string.Format("[ZombiePlus] name={0} type={1} WriteToJsonFile={2}", nameof (its), its.GetType(), filename));
    }

    public static void WriteToJSONFile<T>(HashSet<T> its, string filename)
    {
      string contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename)
    {
      string contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(".\\QMods\\ZombiePlus\\" + filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename, string path = null)
    {
      string contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(path == null ? deployedPath + filename + ".json" : filename + ".json", contents);
    }

    public static void WriteToJSONFile<T>(T its, string filename, string ext = "json", string path = null)
    {
      string contents = JsonConvert.SerializeObject((object) its, Formatting.Indented);
      File.WriteAllText(path == null ? deployedPath + filename + "." + ext : filename + "." + ext, contents);
    }

    public static void WriteDictToFile(Dictionary<string, IList> dic)
    {
      foreach (var keyValuePair in dic)
      {
        Entry.Log(string.Format("[{0}, {1}]", keyValuePair.Key, keyValuePair.Value));
        foreach (var obj in keyValuePair.Value)
          Entry.Log(string.Format("-[{0}]", obj));
      }
    }

    public static void WriteListToFile(IList<Item> list)
    {
      foreach (var obj in list)
        Entry.Log(string.Format("[{0}, {1}]", obj.money, obj));
    }
  }
}
