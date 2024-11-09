using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD.UI
{
    public class NoodUI : MonoBehaviour
    {
        public static T Create<T>(Transform parent = null) where T : NoodUI
        {
            T prefab = Resources.LoadAll<T>("")[0];
            if(prefab == null)
            {
                Debug.LogError("Can't find prefab with type " + typeof(T).Name);
                return null;
            }
            T temp = Instantiate(prefab, parent);
            return temp;
        }
        

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
