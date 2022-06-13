using System;
using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch
{
    public class Logg
    {
        private static readonly string path = "./QMods/AlchemyResearch/Mod Output.txt";

        public static void Log(string content)
        {
            if (!File.Exists(path))
            {
                using (var text = File.CreateText(path))
                    text.WriteLine(content);
            }
            else
            {
                using (var streamWriter = File.AppendText(path))
                    streamWriter.WriteLine(content);
            }
        }

        public static void LogTrace() => Log("Trace: " + Environment.StackTrace.ToString());

        public static void LogRectTransform(RectTransform Transform)
        {
            Log("RectTransform " + Transform.name + ".sizeDelta: " + Transform.sizeDelta.ToString());
            Log("RectTransform " + Transform.name + ".anchorMin: " + Transform.anchorMin.ToString());
            Log("RectTransform " + Transform.name + ".anchorMax: " + Transform.anchorMax.ToString());
            Log("RectTransform " + Transform.name + ".anchoredPosition: " + Transform.anchoredPosition.ToString());
            Log("RectTransform " + Transform.name + ".anchoredPosition3D: " + Transform.anchoredPosition3D.ToString());
            Log("RectTransform " + Transform.name + ".localPosition: " + Transform.localPosition.ToString());
            Log("RectTransform " + Transform.name + ".pivot: " + Transform.pivot.ToString());
            Log("RectTransform " + Transform.name + ".localScale: " + Transform.localScale.ToString());
            Log("RectTransform " + Transform.name + "-Parent: " + Transform.parent.name);
        }

        public static void LogTransform(GameObject Object) => LogTransform(Object.transform);

        public static void LogTransform(Transform Transform)
        {
            Log("Transform " + Transform.name + ".position: " + Transform.position.ToString());
            Log("Transform " + Transform.name + ".rotation: " + Transform.rotation.eulerAngles.ToString());
            Log("Transform " + Transform.name + ".localScale: " + Transform.localScale.ToString());
            Log("Transform " + Transform.name + ".localPosition: " + Transform.localPosition.ToString());
            Log("Transform " + Transform.name + ".localRotation: " + Transform.localRotation.eulerAngles.ToString());
            bool activeSelf;
            if ((bool) (UnityEngine.Object) Transform.gameObject)
            {
                var strArray = new string[6]
                {
                    "Transform ",
                    Transform.name,
                    "-GameObject: ",
                    Transform.gameObject?.ToString(),
                    " - active: ",
                    null
                };
                activeSelf = Transform.gameObject.activeSelf;
                strArray[5] = activeSelf.ToString();
                Log(string.Concat(strArray));
            }
            else
                Log("Transform " + Transform.name + "-GameObject: <none>");

            if ((bool) (UnityEngine.Object) Transform.gameObject)
            {
                var strArray = new string[6]
                {
                    "Transform ",
                    Transform.name,
                    "-Parent: ",
                    Transform.parent.gameObject?.ToString(),
                    " - active: ",
                    null
                };
                activeSelf = Transform.parent.gameObject.activeSelf;
                strArray[5] = activeSelf.ToString();
                Log(string.Concat(strArray));
            }
            else
                Log("Transform " + Transform.name + "-Parent: <none>");
        }

        public static void LogClear()
        {
            using (File.CreateText(path))
                ;
        }

        public static string GetFullPath(Transform Transform, Transform Root = null)
        {
            var fullPath = Transform.name;
            for (var parent = Transform.parent;
                 parent != null && (!(Root != null) || !(parent == Root));
                 parent = parent.parent)
                fullPath = parent.name + "/" + fullPath;
            return fullPath;
        }

        public static void GameObjectInfo(
            MonoBehaviour MonoBehaviour,
            bool ShowComponents = true,
            bool ShowChildren = true,
            bool ShowParents = false,
            int Indentation = 0,
            Transform Root = null)
        {
            GameObjectInfo(MonoBehaviour.gameObject, ShowComponents, ShowChildren, ShowParents, Indentation);
        }

        public static void GameObjectInfo(
            GameObject GameObject,
            bool ShowComponents = true,
            bool ShowChildren = true,
            bool ShowParents = false,
            int Indentation = 0)
        {
            bool activeSelf;
            if (GameObject.transform.parent != null)
            {
                var strArray = new string[9];
                strArray[0] = new string(' ', Indentation);
                strArray[1] = "- ";
                strArray[2] = GameObject.name;
                strArray[3] = " | active: ";
                activeSelf = GameObject.activeSelf;
                strArray[4] = activeSelf.ToString();
                strArray[5] = " | Parent: ";
                strArray[6] = GameObject.transform.parent?.ToString();
                strArray[7] = " | Full Path: ";
                strArray[8] = GetFullPath(GameObject.transform);
                Log(string.Concat(strArray));
            }
            else
            {
                var strArray = new string[6]
                {
                    new string(' ', Indentation),
                    "- ",
                    GameObject.name,
                    " | active: ",
                    null,
                    null
                };
                activeSelf = GameObject.activeSelf;
                strArray[4] = activeSelf.ToString();
                strArray[5] = " | Parent: <none>";
                Log(string.Concat(strArray));
            }

            Indentation += 2;
            if (ShowComponents)
            {
                foreach (object component in GameObject.GetComponents<UnityEngine.Object>())
                    Log(new string(' ', Indentation) + "o " + component.ToString());
            }

            if (ShowChildren)
            {
                for (var index = 0; index < GameObject.transform.childCount; ++index)
                    Log(new string(' ', Indentation) + "C " + GameObject.transform.GetChild(index).ToString());
                for (var index = 0; index < GameObject.transform.childCount; ++index)
                    GameObjectInfo(GameObject.transform.GetChild(index).gameObject, ShowComponents, ShowChildren,
                        ShowParents, Indentation);
            }

            if (!ShowParents || !(GameObject.transform.parent != null) ||
                !(bool) (UnityEngine.Object) GameObject.transform.parent.gameObject)
                return;
            var strArray1 = new string[5]
            {
                GameObject.name,
                " Parent: ",
                GameObject.transform.parent.gameObject.name,
                " | active: ",
                null
            };
            activeSelf = GameObject.transform.parent.gameObject.activeSelf;
            strArray1[4] = activeSelf.ToString();
            Log(string.Concat(strArray1));
            GameObjectInfo(GameObject.transform.parent.gameObject, ShowComponents, ShowChildren, ShowParents,
                Indentation);
        }

        public static void LogObject(object Object)
        {
            Log("Object: " + Object?.ToString());
            var fields = Object.GetType().GetFields(AccessTools.all);
            for (var index = 0; index < fields.Length; ++index)
            {
                var str1 = string.Empty;
                var str2 = string.Empty;
                int num;
                if (fields[index].GetValue(Object) is Array array)
                {
                    num = array.Length;
                    str1 = " | Array Length: " + num.ToString();
                }

                if (fields[index].GetValue(Object) is List<object> objectList)
                {
                    num = objectList.Count;
                    str2 = " | List Count: " + num.ToString();
                }

                Log(" - Field: " + fields[index].Name + " | Value: " + fields[index].GetValue(Object)?.ToString() +
                    str1 + str2);
            }
        }

        public static void LogComponent(UnityEngine.Object Component)
        {
            Log("Component: " + Component?.ToString());
            var fields = Component.GetType().GetFields(AccessTools.all);
            for (var index = 0; index < fields.Length; ++index)
                Log(" - Field: " + fields[index].Name + " | Value: " + fields[index].GetValue(Component)?.ToString());
        }
    }
}