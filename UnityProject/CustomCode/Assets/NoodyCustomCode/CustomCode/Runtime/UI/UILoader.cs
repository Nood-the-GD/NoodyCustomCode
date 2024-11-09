using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.UI;
using NOOD.Data;
using NOOD.Extension;
using System.IO;

namespace NOOD.UI
{
    public class UILoader 
    {
        private static Transform _parentUITransform = null;
        private static Dictionary<string, string> _uiPathDic = new();

#region UISetup
        /// <summary>
        /// Set parent Transform to spawn all UIWindow under parent Transform
        /// </summary>
        public static void SetParentTransform(Transform transform)
        {
            _parentUITransform = transform;
        }
#if UNITY_EDITOR
        public static void SetUIPath(string uiType, string path)
        {
            path = path.Replace("Resources/", "");
            path = path.Replace(".prefab", "");
            if(FileExtension.IsExitFileInDatasFolder("UIDictionary"))
            {
                _uiPathDic = DataManager<Dictionary<string, string>>.LoadDataFromDefaultFolder("UIDictionary");
            }
            if(_uiPathDic.ContainsKey(uiType))
            {
                _uiPathDic[uiType] = path;
            }
            else
            {
                _uiPathDic.Add(uiType, path);
                Debug.Log(_uiPathDic.Keys.Count);
            }
            DataManager<Dictionary<string, string>>.SaveToDefaultFolder(_uiPathDic, "UIDictionary", ".txt");
        }
#endif
#endregion

#region LoadUI
        public static T LoadUI<T>(Transform parent = null) where T : NoodUI
        {
            if(FileExtension.IsExitFileInDatasFolder("UIDictionary"))
            {
                _uiPathDic = DataManager<Dictionary<string, string>>.LoadDataFromDefaultFolder("UIDictionary");
            }


            if(TryGetUI<T>(out T ui))
            {
                // Get UI gamObject in the scene
                if (ui == null || ui.gameObject == null)
                {
                    Transform parentTrans = parent == null ? _parentUITransform : parent;
                    ui = NoodUI.Create<T>(parentTrans);
                    ui.Open();
                    return ui;
                }
                else
                {
                    ui.gameObject.SetActive(true);
                    ui.Open();
                    return ui;
                }
            }
            else
            {
                Debug.Log(_uiPathDic.Count);
                ui = NoodUI.Create<T>(_parentUITransform);
                ui.Open();
                return ui;
            }
        }
#endregion

#region CloseUI
        public static void CloseUI<T>() where T : NoodUI
        {
            if(TryGetUI<T>(out T ui))
                ui?.Close();
        }
#endregion

#region GetUI
        public static bool TryGetUI<T>(out T ui) where T : NoodUI
        {
            T result = GameObject.FindObjectOfType<T>(true);
            if(result != null)
            {
                ui = result;
                return true;
            }
            Debug.Log("Can't find " + typeof(T));
            ui = null;
            return false;
        }
        public static T GetUI<T>() where T : NoodUI
        {
            T result = GameObject.FindObjectOfType<T>(true);
            if(result != null)
            {
                return result;
            }
            Debug.Log("Can't find " + typeof(T));
            return null;
        }
#endregion

    }
}
