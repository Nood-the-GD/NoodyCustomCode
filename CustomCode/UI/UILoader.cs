using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD.UI;

namespace NOOD.UI
{
    public class UILoader 
    {
        private static Dictionary<Type, object> _noodUIDic = new Dictionary<Type, object>();
        private static Transform _parentUITransform;

        #region UISetup
        /// <summary>
        /// Set parent Transform to spawn all UIWindow under parent Transform
        /// </summary>
        public static void SetParentTransform(Transform transform)
        {
            _parentUITransform = transform;
        }
        #endregion

        #region LoadUI
        public static T LoadUI<T>() where T : NoodUI
        {
            if(_noodUIDic.ContainsKey(typeof(T)))
            {
                T ui = GetUI<T>();
                ui.Open();
                return ui;
            }
            else
            {
                T ui = NoodUI.Create<T>(_parentUITransform);
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
