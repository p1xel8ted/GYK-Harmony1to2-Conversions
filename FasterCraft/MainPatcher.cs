﻿using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace FasterCraft
{
    public class MainPatcher
    {
        public static void Patch()
        {
            try
            {
                var harmony = new Harmony("com.glibfire.graveyardkeeper.fastercraft.mod");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FasterCraft (Harmony2)]: {ex.Message}, {ex.Source}, {ex.StackTrace}");
            }
        }
    }
}