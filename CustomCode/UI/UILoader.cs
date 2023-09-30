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
        private static Dictionary<Type, object> _noodUIDic = new Dictionary<Type, object>();
        private static Transform _parentUITransform = null;
        private static Dictionary<string, string> _uiPathDic = new Dictionary<string, string>();

#region UISetup
        /// <summary>
        /// Set parent Transform to spawn all UIWindow under parent Transform
        /// </summary>
        public static void SetParentTransform(Transform transform)
        {
            _parentUITransform = transform;
        }
        public static void SetUIPath(string uiType, string path)
        {
            path = path.Replace("Resources/", "");
            path = path.Replace(".prefab", "");
            if(FileExtension.IsExitFileInDefaultFolder("UIDictionary"))
            {
                _uiPathDic = DataManager<Dictionary<string, string>>.LoadDataFromDefaultFile("UIDictionary");
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
#endregion

#region LoadUI
        public static T LoadUI<T>() where T : NoodUI
        {
            if(FileExtension.IsExitFileInDefaultFolder("UIDictionary"))
            {
                Debug.Log("Load File");
                _uiPathDic = DataManager<Dictionary<string, string>>.LoadDataFromDefaultFile("UIDictionary");
            }

            if(_noodUIDic.ContainsKey(typeof(T)))
            {
                T ui = GetUI<T>();
                ui.Open();
                return ui;
            }
            else
            {
                Debug.Log(_uiPathDic.Count);
                T ui = NoodUI.Create<T>(_uiPathDic[typeof(T).FullName], _parentUITransform);
                ui.Open();
                AddUI(ui);
                return ui;
            }
        }
        private static void AddUI(NoodUI ui)
        {
            if (!_noodUIDic.ContainsKey(ui.GetType()))
            {
                _noodUIDic.Add(ui.GetType(), ui);
            }
        }
#endregion

#region CloseUI
        public static void CloseUI<T>() where T : NoodUI
        {
            T ui = GetUI<T>();
            if(ui != null)
                ui.Close();
        }
#endregion

#region GetUI
        public static T GetUI<T>() where T : NoodUI
        {
            if(_noodUIDic.ContainsKey(typeof(T)))
            {
                return (T) _noodUIDic[typeof(T)];
            }
            Debug.Log("Can't find " + typeof(T));
            return null;
        }
#endregion

    }
}
