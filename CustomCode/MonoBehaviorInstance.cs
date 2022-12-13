using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD
{
    public class MonoBehaviorInstance <T> : AbstractMonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T GetInstace
        {
            get
            {
                if(instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                }

                if (instance == null)
                {
                    GameObject newObj = new GameObject(typeof(T).ToString());
                    instance = newObj.AddComponent<T>();
                }
                return instance;
            }
        }
    }
}

