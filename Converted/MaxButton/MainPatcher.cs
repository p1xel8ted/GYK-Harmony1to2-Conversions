using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MaxButton
{
  public class MainPatcher
  {
    private static readonly string configFilePathAndName = Application.dataPath + "/../QMods/MaxButton/config.txt";
    private static readonly char parameterSeparator = '=';
    private const string parameterMaximumButtonName = "MaxButtonName";

    public static void Patch()
    {

      var harmony = new Harmony("com.graveyardkeeper.urbanvibes.maxbutton");
      var assembly = Assembly.GetExecutingAssembly();
      harmony.PatchAll(assembly);
            MainPatcher.ReadParametersFromFile();
    }

    public static void ReadParametersFromFile()
    {
      string empty = string.Empty;
      string[] strArray1;
      try
      {
        strArray1 = File.ReadAllLines(MainPatcher.configFilePathAndName);
      }
      catch (Exception ex)
      {
        return;
      }
      if (strArray1 == null || strArray1.Length == 0)
        return;
      foreach (string str1 in strArray1)
      {
        string str2 = str1.Trim();
        if (!string.IsNullOrEmpty(str2))
        {
          string[] strArray2 = str2.Split(MainPatcher.parameterSeparator);
          if (strArray2.Length >= 2 && strArray2[0].Trim() == "MaxButtonName")
          {
            MaxButtonVendor.MaxButtonName = strArray2[1].Trim();
            break;
          }
        }
      }
    }
  }
}
